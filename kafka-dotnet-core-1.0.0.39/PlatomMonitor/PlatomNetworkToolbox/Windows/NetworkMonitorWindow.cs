using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;
using Newtonsoft.Json;
using Platom.Protocol;
using Platom.Protocol.Schema;
using Platom.Protocol.Schema.Validators;
using PlatomMonitor.PROTO;
using PlatomMonitor.Topology;
using PlatomMonitor.Topology.SimpleMonitor;
using PlatomMonitor.Windows.Models;
using SimpleLogger;

namespace PlatomMonitor.Windows
{
    public partial class NetworkMonitorWindow : Form, ITopologyObserver
    {
        private ILogger logger;
        //private SchemaValidatorLoader schema_loader;
        private ServicesActivityMonitor service_monitor;
        private SimpleTopologyMonitor topology_monitor;

        private Thread thread_status;
        private CancellationTokenSource status_cts;

        private string filter_service, filter_channel;
        private object filter_sync;
        private object consumer_sync;
        private IConsumer<Ignore, string>  consumer;

        private BindingList<LoggedMessage> logged_messages;

        private Dictionary<string, ServiceNode> service2node;
        private Dictionary<string, ChannelNode> channel2node;
        private List<SchemaEntry> available_schamas;


        public NetworkMonitorWindow()
        {
            InitializeComponent();
            this.logger = this.log_view;
            this.available_schamas = new List<SchemaEntry>();

            //
            // Przygotowanie środowiska
            //this.schema_loader = new SchemaValidatorLoader();

            this.topology_monitor = new SimpleTopologyMonitor();
            this.topology_monitor.RegisterServiceActivityObserver(this);
            this.service_monitor = new ServicesActivityMonitor();
            this.service_monitor.RegisterServiceActivityObserver(this.topology_monitor);

            this.filter_sync = new object();
            this.consumer_sync = new object();

            this.logged_messages = new BindingList<LoggedMessage>();

            this.service2node = new Dictionary<string, ServiceNode>();
            this.channel2node = new Dictionary<string, ChannelNode>();

            //DataTable dt = new DataTable("x");
            //dt.Columns.Add("timestamp", typeof(DateTime));
            //dt.Columns.Add("service", typeof(string));
            //dt.Columns.Add("channel", typeof(string));
            //dt.Columns.Add("seq_number", typeof(long));
            //dt.Columns.Add("content", typeof(string));


            //for (int i = 0; i < 10; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["timestamp"] = DateTime.Now;
            //    dt.Rows.Add(dr);
            //}

            this.LoggedMessageBindingSource.DataSource = this.logged_messages;
        }

        private void button_clear_logs_Click(object sender, EventArgs e)
        {
            this.log_view.Clear();
        }

        #region System logów

        #endregion

        private void NetworkMonitorWindow_Load(object sender, EventArgs e)
        {

            this.status_cts = new CancellationTokenSource();
            this.thread_status = new Thread(new ThreadStart(this.StatusConsumerThreadRoutine));
            this.thread_status.Start();


            this.logger.Information("Start sesji");
            this.logger.Warning("Trwa łączenie z brokerem Kafka...");
            this.treeView1.Nodes.Clear();
            this.option_log_clicked(null, null);
        }



        private void StatusConsumerThreadRoutine()
        {

            ConsumerConfig config = new ConsumerConfig()
            {
                BootstrapServers = Platom.Configuration.KafkaServerPortForm,
                GroupId = "$NetworkMonitor-" + Guid.NewGuid(),
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnablePartitionEof = true
            };

            CancellationToken cancellation_token = this.status_cts.Token;

            using (this.consumer = new ConsumerBuilder<Ignore, string>(config)
                // Note: All handlers are called on the main .Consume thread.
                .SetErrorHandler((_, e) => this.logger.Error($"Błąd komunikacja Kafka: {e.Reason}"))
                .SetStatisticsHandler((_, json) => ProcessStatisticsMessage(json))
                .SetPartitionsAssignedHandler((c, partitions) => { logger.Information($"Assigned partitions: [{string.Join(", ", partitions)}]"); })
                .SetPartitionsRevokedHandler((c, partitions) => { logger.Information($"Revoking assignment: [{string.Join(", ", partitions)}]"); })
                .Build())
            {
                //
                SubscribeChannel();

                while (!cancellation_token.IsCancellationRequested)
                {
                    try
                    {
                        ConsumeResult<Ignore, string> consume_result;
                        lock (this.consumer_sync)
                            consume_result = consumer.Consume(cancellation_token);

                        if (consume_result.IsPartitionEOF)
                        {
                            this.logger.Warning(
                                $"Osiągnięto koniec tematu {consume_result.Topic}, partycja {consume_result.Partition}, offset {consume_result.Offset}.");
                            continue;
                        }

                        if (consume_result.Topic == "status")
                            ProcessStatusMessage(consume_result.Timestamp, consume_result.Value, consume_result.Topic);
                        else
                            ProcessDataMessage(consume_result.Timestamp, consume_result.Value, consume_result.Topic);

                        try
                        {
                            lock (this.consumer_sync)
                                consumer.Commit(consume_result);
                        }
                        catch (KafkaException e)
                        {
                            this.logger.Error($"Błąd commit: {e.Error.Reason}");
                        }
                        //

                    }
                    catch (Exception ex)
                    {
                        this.logger.Error($"Wyjątek komunikacji: {ex.Message}");
                    }
                }
            }

        }

        private void SubscribeChannel(string dataChannelName = null)
        {
            string[] names = null;
            if (string.IsNullOrEmpty(dataChannelName))
                names = new string[] {"status"};
            else
                names = new string[] {"status", dataChannelName};

            names = names.Distinct().ToArray();

            //this.consumer.Assign();
            TopicPartition[] p = names.Select(x => new TopicPartition(x, new Partition(0))).ToArray();
            lock (this.consumer_sync)
                this.consumer.Assign(p);
            //              this.consumer.Subscribe(names);

        }


        private void ProcessStatusMessage(Timestamp timestmap, string content, string channelName)
        {
            // Jeżeli lista schamtów jest pusta to oznacza, że procedura aktualizacji
            // z repo gita jeszcze nie zakończyłą się. Należy więc zignorować komunikat.
            lock(this.available_schamas)
                if (this.available_schamas.Count == 0)
                    return;

            try
            {
                lock (this.available_schamas)
                {
                    SchemaEntry se = this.available_schamas.Find(x => x.SchemaName == "schema_status");
                    if (se == null)
                        throw new ValidatorException("Brak schmatu `schema_status`");
                    SchemaValidator sv = new SchemaValidator(se.Text);
                    sv.ValidateMessage(content);
                }
            }
            catch (ValidatorException ve)
            {
                this.logger.Error($"Błąd walidacji komunikatu w kanale '{channelName}': {ve.Message}");
                this.logger.ErrorSupplement(content);
                return;
            }

            //
            // Parsowanie komunikatu kanału status
            StatusMessage sm = null;
            try
            {
                // Używaj atrybutu DefaultValueAttribute
                JsonSerializerSettings jss = new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
                };
                sm = JsonConvert.DeserializeObject<StatusMessage>(content, jss);
            }
            catch (JsonReaderException jre)
            {
                // todo: raportowanie błędów
            }

            this.logger.Information($"Poprawny komunikat od usługi [{sm.Sequence.ServiceName}] otrzymany kanałem [{sm.Sequence.ChannelName}]");

            this.service_monitor.Update(sm);
            //
            //

            // Filtrowanie
            bool match = false;
            lock (this.filter_sync)
                match = sm.Sequence.ServiceName == this.filter_service && sm.Sequence.ChannelName == this.filter_channel;

            if (match)
                this.Invoke(new Action<StatusMessage
                    , string>(LogPlatomMessage), sm, content);
        }

        private void LogPlatomMessage(StatusMessage sm, string content)
        {
            LoggedMessage lm = new LoggedMessage(sm.Sequence, content);
            this.logged_messages.Add(lm);
            this.dataGridView2.FirstDisplayedScrollingRowIndex = this.dataGridView2.RowCount - 1;
        }

        private void ProcessDataMessage(Timestamp timestamp, string content, string channelName)
        {
            // Jeżeli lista schamtów jest pusta to oznacza, że procedura aktualizacji
            // z repo gita jeszcze nie zakończyłą się. Należy więc zignorować komunikat.
            lock (this.available_schamas)
                if (this.available_schamas.Count == 0)
                    return;


            // 
            // Wstępna deserializacja
            //
            //
            try
            {
                // Używaj atrybutu DefaultValueAttribute
                JsonSerializerSettings jss = new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
                };
                // Parsowanie komunikatu kanału status
                CommonMessage cm = JsonConvert.DeserializeObject<CommonMessage>(content, jss);
                SchemaValidator.ValidateMessageSequence(cm);


                // ok, nagłówek jest poprawny; teraz dalej
                // Filtrowanie
                bool match = false;
                lock (this.filter_sync)
                    match = cm.Sequence.ServiceName == this.filter_service && cm.Sequence.ChannelName == this.filter_channel;
                if (!match)
                    return; // ten komunikat nas nie interesuje

                this.Invoke(new Action<LoggedMessage>(this.logged_messages.Add), new LoggedMessage(
                    cm.Sequence, content));


                lock (this.available_schamas)
                {
                    SchemaEntry se = this.available_schamas.Find(x => x.SchemaName == cm.Sequence.SchemaName);
                    if (se == null)
                        throw new ValidatorException($"Brak schmatu `{cm.Sequence.SchemaName}`");
                    SchemaValidator sv = new SchemaValidator(se.Text);
                    sv.ValidateMessage(content);
                }


            }
            catch (JsonReaderException jre)
            {
                // todo: raportowanie błędów
            }
            catch (ValidatorException ve)
            {
                this.logger.Error($"Błąd walidacji komunikatu w kanale '{channelName}': {ve.Message}");
                this.logger.ErrorSupplement(content);
                return;
            }

        }



        private void ProcessStatisticsMessage(string json)
        {
            try
            {
                Platom.Protocol.Kafka.Statistics.Root stats = JsonConvert.DeserializeObject<Platom.Protocol.Kafka.Statistics.Root>(json);
                //TODO co z tym zrobić?
            }
            catch (Exception ex)
            {

            }
        }

        private void NetworkMonitorWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //
            // Schemat działania: ukryj okno i wyłącz wszystkie połączenia/monitory a potem faktycznie zamknij okno
            //

            if (!this.Visible)
                return;

            e.Cancel = true;
            this.Visible = false;

            new Thread(new ThreadStart(() =>
            {
                // 1. Zatrzymaj wątek generujący strumień komunikatów dla mechanizmów monitorujących
                this.status_cts.Cancel();
                this.thread_status.Join();

                // 2. Zatrzymaj wszystkie monitory
                this.topology_monitor.Dispose();
                this.service_monitor.Dispose();

                this.Invoke(new Action(this.Close));
            })).Start();
        }

        #region ITopologyObserver

    

        void ITopologyObserver.OnServiceDiscovery(string serviceName)
        {
            if (this.service2node.ContainsKey(serviceName))
                return;

            ServiceNode sn = new ServiceNode(serviceName);
            ChannelNode channel_node = new ChannelNode(serviceName, "status", Visuals.IMAGE_STATUS);

            sn.Nodes.Add(channel_node);
            //sn.ExpandAll();

            string channel_name = $"{serviceName}/status";
            this.channel2node.Add(channel_name, channel_node);

            this.service2node.Add(serviceName, sn);
            this.Invoke(new Func<TreeNode, int>(this.treeView1.Nodes.Add), sn);
        }

        void ITopologyObserver.OnServiceActivated(string serviceName)
        {
            if (!this.service2node.ContainsKey(serviceName))
                return;
            
            this.service2node[serviceName].SetActive(true);
        }

        void ITopologyObserver.OnServiceTimeout(string serviceName)
        {
            if (!this.service2node.ContainsKey(serviceName))
                return;

            this.service2node[serviceName].SetActive(false);
        }


        void ITopologyObserver.OnPublicationChannelAdded(string serviceName, string channelName)
        {
            if (!this.service2node.ContainsKey(serviceName))
                return;

            string name = $"{serviceName}/{channelName}";
            if (this.channel2node.ContainsKey(name))
            {
                ChannelNode channel_node = this.channel2node[name];
                channel_node.SetActive(true);
                this.Invoke(new Action<int>(channel_node.SetImage), Visuals.IMAGE_PUBLICATION);
            }
            else
            {
                ChannelNode cn = new ChannelNode(serviceName, channelName, Visuals.IMAGE_PUBLICATION);
                this.channel2node.Add(name, cn);

                ServiceNode parent = this.service2node[serviceName];
                this.Invoke(new Func<TreeNode, int>(parent.Nodes.Add), cn);
            }
        }

        void ITopologyObserver.OnPublicationChannelRemoved(string serviceName, string channelName)
        {
            if (!this.service2node.ContainsKey(serviceName))
                return;

            string name = $"{serviceName}/{channelName}";
            if (!this.channel2node.ContainsKey(name))
                return;

            ChannelNode channel_node = this.channel2node[name];
            channel_node.SetActive(false);
        }

        void ITopologyObserver.OnSubscriptionChannelAdded(string serviceName, string channelName)
        {
            if (!this.service2node.ContainsKey(serviceName))
                return;

            string name = $"{serviceName}/{channelName}";
            if (this.channel2node.ContainsKey(name))
            {
                ChannelNode channel_node = this.channel2node[name];
                channel_node.SetActive(true);
                this.Invoke(new Action<int>(channel_node.SetImage), Visuals.IMAGE_SUBSCRIPTION);
            }
            else
            {
                ChannelNode cn = new ChannelNode(serviceName, channelName, Visuals.IMAGE_SUBSCRIPTION);
                this.channel2node.Add(name, cn);

                ServiceNode parent = this.service2node[serviceName];
                this.Invoke(new Func<TreeNode, int>(parent.Nodes.Add), cn);
            }
        }

        void ITopologyObserver.OnSubscriptionChannelRemoved(string serviceName, string channelName)
        {
            if (!this.service2node.ContainsKey(serviceName))
                return;

            string name = $"{serviceName}/{channelName}";
            if (!this.channel2node.ContainsKey(name))
                return;

            ChannelNode channel_node = this.channel2node[name];
            channel_node.SetActive(false);
        }

        #endregion

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ChannelNode)
            {
                ChannelNode cn = e.Node as ChannelNode;
                this.SetFilter(cn.ServiceName, cn.ChannelName);
                this.SubscribeChannel(cn.ChannelName);
            }
        }

        private void SetFilter(string serviceName, string channelName)
        {
            lock (this.filter_sync)
            {
                this.filter_service = serviceName;
                this.filter_channel = channelName;

                this.label7.Text = this.filter_service;
                this.label9.Text = this.filter_channel;
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            LoggedMessage lm = this.logged_messages[e.RowIndex];
            this.textBox1.Text = lm.Content;

        }

        private void NetworkMonitorWindow_Shown(object sender, EventArgs e)
        {
            using (RepositoryUpdater updater = new RepositoryUpdater())
            {
                updater.StartUpdate(true, true);
                lock (this.available_schamas)
                {
                    this.available_schamas.Clear();
                    this.available_schamas.AddRange(updater.Examples);
                }
            }

        }

        private void option_log_clicked(object sender, EventArgs e)
        {
            this.log_view.AutoScrol = this.option_log_autoscroll.Checked;
            this.log_view.IsActive = this.option_log_active.Checked;
            this.log_view.ShowOnlyErrors = this.option_only_errors.Checked;
        }
    }

    class ServiceNode : TreeNode
    {
        public ServiceNode(string serviceName)
            : base(serviceName, Visuals.IMAGE_SERVICE, Visuals.IMAGE_SERVICE)
        {
            this.ForeColor = Visuals.COLOR_SERVICE_ACTIVE;
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
                base.ForeColor = Visuals.COLOR_SERVICE_ACTIVE;
            else
                base.ForeColor = Visuals.COLOR_SERVICE_INACTIVE;
        }
    }

    class ChannelNode : TreeNode
    {
        private string service_name;
        private string channe_name;

        public string ChannelName => this.channe_name;
        public string ServiceName => this.service_name;

        public ChannelNode(string serviceName, string channelName, int image)
            : base(channelName, image, image)
        {
            this.service_name = serviceName;
            this.channe_name = channelName;
            this.ForeColor = Visuals.COLOR_CHANNEL_ACTIVE;
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
                base.ForeColor = Visuals.COLOR_CHANNEL_ACTIVE;
            else
                base.ForeColor = Visuals.COLOR_CHANNEL_INACTIVE;

        }

        public void SetImage(int imageIndex)
        {
            base.ImageIndex = imageIndex;
            base.SelectedImageIndex = imageIndex;
        }
    }

    internal static class Visuals
    {
        public static  readonly int IMAGE_SERVICE = 2;
        public static readonly int IMAGE_PUBLICATION = 5;
        public static readonly int IMAGE_SUBSCRIPTION = 1;
        public static readonly int IMAGE_STATUS = 6;
        public static readonly Color COLOR_SERVICE_ACTIVE = Color.Black;
        public static readonly Color COLOR_SERVICE_INACTIVE = Color.Gray;

        public static readonly Color COLOR_CHANNEL_ACTIVE = Color.Black;
        public static readonly Color COLOR_CHANNEL_INACTIVE = Color.Gray;
    }

    public class LoggedMessage
    {
        public MessageSequence Sequence { get; private set; }
        public string Content { get; private set; }

        public DateTime Timestamp => this.Sequence.TimeStamp;
        public string ServiceName => this.Sequence.ServiceName;
        public string ChannelName => this.Sequence.ChannelName;
        public long SequneceNumber => this.Sequence.SequenceNumber.Value;


        public LoggedMessage(MessageSequence sequence, string content)
        {
            this.Sequence = sequence;
            this.Content = content;
        }
    }



}
