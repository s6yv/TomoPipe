using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;
using Platom.Protocol;
using Platom.Protocol.Schema;
using Platom.Protocol.Schema.Loader;
using Platom.Protocol.Schema.Validators;
using PlatomMonitor.Windows.Models;
using SimpleLogger;

//using LibGit2Sharp;
//using LibGit2Sharp.Handlers;

namespace PlatomMonitor.Windows
{
    public partial class RepositoryUpdater : Form
    {
        private string url;
        private string message;
        private object message_sync;
        private ILogger log;
        private SchemaValidatorProvider svp;

        private bool hide_after_completion;
        private bool visual_delay;

        public List<SchemaEntry> Examples { get; internal set; }
        public SchemaValidatorProvider ValidatorProvider { get; internal set; }

        public RepositoryUpdater(/*bool autoHide*/)
        {
            this.message = "Pobieranie schematów walidacyjnych i przykładów...";
            this.message_sync = new object();
            this.visual_delay = false;

            InitializeComponent();

            url = "https://gitlabplatom.cti.p.lodz.pl/tjaworski/validation_schemas.git";
            //url = "https://github.com/reactos/reactos.git";

            this.label2.Text = $"Pobieranie repozytorium schematów walidcyjnych z {url}...";
            this.log = this.loggerView1;
        }

        public void StartUpdate(bool hideAfterCompletion, bool visualDelay)
        {
            this.hide_after_completion = hideAfterCompletion;
            this.button_close.Enabled = !hideAfterCompletion; // deaktywuj przycisk, jeżeli okno ma się samo zamknąć
            this.visual_delay = visualDelay;

            if (this.InvokeRequired)
            {
                //this.Invoke(new Action(this.backgroundWorker1.RunWorkerAsync));
                this.Invoke(new Func<DialogResult>(this.ShowDialog));
            }
            else
            {
                this.ShowDialog();
            }
        }

        private void DeleteDirecotry(string path)
        {
            // Skanuj pliki
            foreach (string file_name in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
            {
                // Wyłącz atrybuty H, S, RO (jeżeli są)
                FileAttributes fs = File.GetAttributes(file_name);
                uint hsro = (uint) FileAttributes.Hidden | (uint) FileAttributes.System | (uint) FileAttributes.ReadOnly;
                if (((uint) fs & hsro) != 0)
                    File.SetAttributes(file_name, (FileAttributes) ((uint) fs & ~hsro));

                // usuń plik
                File.Delete(file_name);
            }

            // Skanuj katalogi
            foreach (string dir_name in Directory.GetDirectories(path, "*.*", SearchOption.TopDirectoryOnly))
            {
                DeleteDirecotry(dir_name);
                Directory.Delete(dir_name);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {

                this.log.Information($"Usuwanie istniejącego repozytorium {Platom.Configuration.ValidationSchemasRepositoryPath}...");
                this.backgroundWorker1.ReportProgress(0, "Usuwanie istniejącego repozytorium");
                DeleteDirecotry(Platom.Configuration.ValidationSchemasRepositoryPath);

                this.log.Information($"Klonowanie {url}...");
                this.backgroundWorker1.ReportProgress(0, $"Klonowanie {url}...");

                if (this.visual_delay)
                    Thread.Sleep(2000);

                CloneOptions co = new CloneOptions();
                co.OnTransferProgress = progress =>
                {
                    this.backgroundWorker1.ReportProgress(0, $"{progress.ReceivedObjects}/{progress.TotalObjects}, {progress.ReceivedBytes} bajtów");
                    return true;
                };

                co.OnCheckoutProgress = (string path, int completedSteps, int totalSteps) =>
                {
                    this.backgroundWorker1.ReportProgress(0, $"Zapis pliku {path}");
                };
                co.OnProgress = progress =>
                {
                    int idx = progress.IndexOfAny(new char[]
                    {
                        '\r', '\r'
                    });
                    if (idx == -1)
                        this.backgroundWorker1.ReportProgress(0, progress.Trim());
                    else
                        this.backgroundWorker1.ReportProgress(0, progress.Substring(0, idx).Trim());
                    return true;
                };

                Repository.Clone(url, Platom.Configuration.ValidationSchemasRepositoryPath, co);

                using (Repository repo = new Repository(Platom.Configuration.ValidationSchemasRepositoryPath))
                {
                    // resetuj repo na mastera, usuń wszystkie zmiany lokalne
                    Commit master_head = repo.Branches["master"].Tip;
                    repo.Reset(ResetMode.Hard, master_head);

                    PullOptions options = new PullOptions();
                    options.FetchOptions = new FetchOptions();

                    Signature signature = new Signature("Platform Network Toolkip", "platom2018@iis.p.lodz.pl", DateTimeOffset.Now);
                    Commands.Pull(repo, signature, options);
                    master_head = repo.Branches["master"].Tip;

                    this.log.Warning(
                        $"Ostatni commit w gałęzi master: {master_head.Message.Trim()} ({master_head.Sha}, {master_head.Author.Name.Trim()})");

                }

                this.log.InformationSuccess("Repozytorium zostało pobrane pomyślnie.");

                this.backgroundWorker1.ReportProgress(0, "Pobieranie zakończone");


                if (this.visual_delay)
                    Thread.Sleep(2000);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd", "Wystąpił błąd podczas pobierania repozytorium schematów walidacyjnych:\r\n" + ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //
            //
            //


            string search_path = Platom.Configuration.ValidationSchemasRepositoryPath;
            string[] files = Directory.GetFiles(search_path, "*.json", SearchOption.AllDirectories);
            List<Entry> entries = new List<Entry>();

            this.log.Information(
                $"W katalogu {search_path} znaleziono {files.Length} plików o rozszerzeniu '*.json'. Wyszukiwanie schematów walidacyjnych...");

            SchemaValidatorProvider new_svp = new SchemaValidatorProvider();
            List<Models.SchemaEntry> examples = new List<SchemaEntry>();

            // spróbuj podzielić pliki na schematy i komunikaty
            foreach (string file_path in files)
            {
                //string content = File.ReadAllText(file_path);

                try
                {
                    SchemaValidator sv = new_svp.LoadSchemaFromFile(file_path, LoadMode.LoadOnly);

                    string short_file_path = file_path.Substring(search_path.Length);
                    this.log.Information($"* Plik {short_file_path} => schemat {sv.SchemaName}");

                    //
                    // Dodaj walidator do listy
                    try
                    {
                        new_svp.LoadSchemaFromFile(file_path, LoadMode.LoadAndStore);
                        examples.Add(new SchemaEntry()
                        {
                            FileName = file_path,
                            ServiceName = sv.ServiceName,
                            SchemaName = sv.SchemaName,
                            Text = sv.SchemaText
                        });
                    }
                    catch (SchemaValidatorProviderException svle)
                    {
                        this.log.Error($"Błąd wczytywania schematu z {file_path}");
                        this.log.ErrorSupplement(svle.Message);
                    }
                }
                catch (SchemaValidatorProviderException svle)
                {
                    //
                }
                catch (ValidatorException ve)
                {
                    //
                }
            }


            this.log.InformationSuccess($"Znaleziono {examples.Count} schematów walidacyjnych");

            //
            //


            this.log.Information($"Wyszukiwanie przykładowych komunikatów...");
            foreach (string file_path in files)
            {
                string content = File.ReadAllText(file_path);

                SchemaValidator sv = new_svp.TryMatchValidator(content);
                if (sv == null)
                    continue;

                CommonMessage_DictionaryPayload cm = SchemaValidator.DeserializeAsDictionaryPayload(content);
                
                string short_file_path = file_path.Substring(search_path.Length);
                this.log.Information($"* Plik {short_file_path} => schemat {sv.SchemaName}");

                Models.SchemaEntry se = examples.Find(x => x.SchemaName == sv.SchemaName);
                Debug.Assert(se != null);
                se.Messages.Add(new MessageEntry()
                {
                    FileName = file_path,
                    SchemaName = sv.SchemaName,
                    ServiceName = sv.ServiceName,
                    ChannelName = cm.Sequence.ChannelName,
                    Text = content
                });

            }

            int example_count = examples.Select(x => x.Messages.Count).Sum();
            this.log.InformationSuccess($"Znaleziono {example_count} przykładowych komunikatów");
            this.log.InformationSuccess("Gotowe.");

            this.ValidatorProvider = new_svp;
            this.Examples = examples;

            if (this.hide_after_completion)
            {
                if (this.visual_delay)
                    Thread.Sleep(3000);
                this.Invoke(new Action(this.Hide));
            }
        }



        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (this.message_sync)
                this.message = e.UserState as string;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string msg;
            lock (this.message_sync)
                msg = this.message;

            this.label_message.Text = msg;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void RepositoryUpdater_Shown(object sender, EventArgs e)
        {
            this.backgroundWorker1.RunWorkerAsync();
        }
    }


}

