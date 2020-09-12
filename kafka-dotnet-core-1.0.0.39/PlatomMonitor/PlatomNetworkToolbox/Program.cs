using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlatomMonitor.Windows;

namespace PlatomMonitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new NetworkMonitorWindow());
            //Application.Run(new RepositoryUpdater(true));
            Application.Run(new MainWindow());
        }
    }
}
