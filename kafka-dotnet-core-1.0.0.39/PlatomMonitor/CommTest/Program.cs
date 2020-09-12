using System;
using System.Diagnostics;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Platom.Protocol;

using Platom.Protocol;
using Platom.Protocol.Schema;
using Platom.Protocol.Schema.Validators;
using PlatomMonitor.PROTO;
using Platom.Protocol.Kafka.Statistics;
using PlatomMonitor.Topology.SimpleMonitor;

namespace CommTest
{
    class Program
    {

        static void WaitForKey()
        {
            while (!Console.KeyAvailable)
                System.Threading.Thread.Sleep(100);
        }

        static void Main(string[] args)
        {
            //NativeConsole.InitConsoleHandles();


            
            TestClass test = new TestClass();
            //test.a();


            Console.WriteLine("Start...");
            while (true)
            {

                WaitForKey();
                char ch = Console.ReadKey(true).KeyChar;
                if (Char.ToUpper(ch) == 'Q')
                    break;
                if (ch == 'c')
                {
                    test.AddConsoleObserver();
                }
                if (ch == 'g')
                {
                    Console.Write("GC[");
                    GC.Collect();
                    Console.WriteLine("]");
                }

            }

            Process.GetCurrentProcess().Kill();
        }




    }

    class TestClass
    {
        private Thread thread_status;
        private ServicesActivityMonitor sam;
        private SimpleTopologyMonitor topology;
        private int id = 0;


        //private SchemaValidatorLoader svl;

        public TestClass()
        {

            //this.svl = new SchemaValidatorLoader();

            this.sam = new ServicesActivityMonitor();
            this.sam.RegisterServiceActivityObserver(this.topology = new SimpleTopologyMonitor());


            this.thread_status = new Thread(new ThreadStart(this.StatusConsumerThreadRoutine));
            this.thread_status.Start();



            //
            //
            

        }

        public void AddConsoleObserver()
        {
            this.sam.RegisterServiceActivityObserver(new ConsoleServiceActivityObserver(id++));
        }

        private void StatusConsumerThreadRoutine()
        {

            ConsumerConfig config = new ConsumerConfig()
            {
                BootstrapServers = Platom.Configuration.KafkaServerPortForm,
                GroupId = "$NetworkMonitor-"+ Guid.NewGuid() ,
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnablePartitionEof = true
            };




            using (var consumer = new ConsumerBuilder<Ignore, string>(config)
                // Note: All handlers are called on the main .Consume thread.
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => ProcessStatisticsMessage(json))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                    // possibly manually specify start offsets or override the partition assignment provided by
                    // the consumer group by returning a list of topic/partition/offsets to assign to, e.g.:
                    // 
                    // return partitions.Select(tp => new TopicPartitionOffset(tp, externalOffsets[tp]));
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                })
                .Build())
            {

                //
                consumer.Subscribe("status");
                while (true)
                {
                    try
                    {
                        ConsumeResult<Ignore, string> consumeResult = consumer.Consume();
                        
                        if (consumeResult.IsPartitionEOF)
                        {
                            //   Console.WriteLine(
                            //     $"Reached end of topic {consumeResult.Topic}, partition {consumeResult.Partition}, offset {consumeResult.Offset}.");

                            continue;
                        }

                        //Console.WriteLine($"{consumeResult.Topic}: {consumeResult.Value.Substring(0,32)}...");
                        ProcessStatusMessage(consumeResult.Timestamp, consumeResult.Value);
                        //Console.WriteLine(
                        //$"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Value}");


                        try
                        {
                            consumer.Commit(consumeResult);
                        }
                        catch (KafkaException e)
                        {
                            Console.WriteLine($"Commit error: {e.Error.Reason}");
                        }
                        //

                    }
                    catch (Exception ex)
                    {
                        ///
                        Console.WriteLine();
                    }
                }

            }

        }

        private void ProcessStatusMessage(Timestamp timestmap, string content)
        {
            try
            {
               // SchemaValidator sv = svl.LoadFromDisk("schema_status");
                //sv.ValidateMessage(content);
            }
            catch (ValidatorException ve)
            {
                Console.WriteLine($"Błąd walidacji komunikatu w kanale 'status': {ve.Message}");
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


            this.sam.Update(sm);
            //
            //
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

    }

    /*


    public class ChannelEventArgs
    {
        public string ChannelName { get; private set; }

        public ChannelEventArgs(string name)
        {
            this.ChannelName = name;
        }
    }


    public class ChannelCloseEventArgs : ChannelEventArgs
    {
        public ChannelCloseEventArgs(string name)
            : base(name)
        {
            //
        }
    }

    public enum ChannelOpenEventType
    {
        ReopenEvent,
        CatchupEvent,
        //???
    }

    public class ChannelOpenEventArgs : ChannelEventArgs
    {
        //    private ChannelOpenEventType event_type;

        //  public string ServiceName { get; private set; }
        //public bool IsCatchupEvent => this.event_type == ChannelOpenEventType.CatchupEvent;
        // public bool ReopenEvent => this.event_type == ChannelOpenEventType.ReopenEvent;

        public ChannelOpenEventArgs(string channelName, string serviceName, ChannelOpenEventType eventType)
            : base(channelName)
        {
            //this.ServiceName = serviceName;
            //this.event_type = eventType;
        }
    }

    //public class SinkChannelEventArgs : ChannelEventArgs
    */
}

