using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;


namespace PlatomMonitor.Windows
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            this.InitializeComponent();

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                this.Text += $"; Wersja: {System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion}";

        }


        private void button_info_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (AboutBox1 wnd = new AboutBox1())
                wnd.ShowDialog();
        }

        private void button_run_validators_Click(object sender, EventArgs e)
        {
            using (ValidatorToolWindow wnd = new ValidatorToolWindow())
                wnd.ShowDialog();
        }

        private void button_show_monitor_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Funkcjonalność w trakcie przygotowania.\r\nCzy mimo to chcesz uruchomić?", "Pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            //    DialogResult.No)
            //    return;

            //if (MessageBox.Show("Funkcje mogą nie działać albo działać inaczej niż powinny.\r\nCzy mimo to chcesz uruchomić?", "Pytanie",
            //        MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            //    DialogResult.No)
            //    return;

            //if (MessageBox.Show("I coś może wybuchnąć.\r\nCzy mimo to chcesz uruchomić?", "Poważne pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            //    DialogResult.No)
            //    return;


            //if (MessageBox.Show("Serio?", "Bardzo poważne pytanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
            //    DialogResult.No)
            //    return;

            //MessageBox.Show("Żeby nie było - ostrzegałem.", "Konkret informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (NetworkMonitorWindow wnd = new NetworkMonitorWindow())
                    wnd.ShowDialog();
            }
            catch (ObjectDisposedException ode)
            {
                // 
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            this.button_run_validators.Focus();
        }
    }

}
