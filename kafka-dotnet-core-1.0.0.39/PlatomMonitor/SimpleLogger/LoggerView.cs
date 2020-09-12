using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleLogger
{
    public partial class LoggerView : UserControl, ILogger
    {
        [DefaultValue(true), Category("Logger")]
        public bool IsActive { get; set; }

        [DefaultValue(true), Category("Logger")]
        public bool AutoScrol { get; set; }

        [DefaultValue(false), Category("Logger")]
        public bool ShowOnlyErrors { get; set; }


        public LoggerView()
        {
            InitializeComponent();
            this.IsActive = true;
            this.AutoScrol = true;
            this.ShowOnlyErrors = false;
        }


        public void Information(string comment)
        {
            if (this.ShowOnlyErrors)
                return;

            string msg = $"{DateTime.Now.ToShortTimeString()}: {comment.Trim()}";
            this.AppendToLogWindow(msg, Color.White, 0);
        }

        public void InformationSuccess(string comment)
        {
            if (this.ShowOnlyErrors)
                return;
            string msg = $"{DateTime.Now.ToShortTimeString()}: {comment.Trim()}";
            this.AppendToLogWindow(msg, Color.Lime, 0);
        }

        public void Error(string comment)
        {
            string msg = $"{DateTime.Now.ToShortTimeString()}: {comment.Trim()}";
            this.AppendToLogWindow(msg, Color.Red, 0);
        }

        public void ErrorSupplement(string comment)
        {
            this.AppendToLogWindow(comment.Trim(), Color.WhiteSmoke, 32);
        }


        public void Warning(string comment)
        {
            if (this.ShowOnlyErrors)
                return;
            string msg = $"{DateTime.Now.ToShortTimeString()}: {comment.Trim()}";
            this.AppendToLogWindow(msg, Color.Yellow, 0);
        }

     

        private void AppendToLogWindow(string msg, Color color, int indentation)
        {
            if (this.InvokeRequired)
            {
                if (this.IsDisposed)
                    return;
                this.Invoke(new Action<string, Color, int>(AppendToLogWindow), msg, color, indentation);
                return;
            }

            // Czy logowanie jest aktywne?
            if (!this.IsActive)
                return;

            // Jeżeli log jest zbyt długi, to usuń najstarsze teksty
            int max_len = 32 * 1024;
            if (this.view_logs.TextLength > max_len)
            {
                this.view_logs.SelectionStart = 0;
                this.view_logs.SelectionLength = this.view_logs.TextLength - max_len;
                this.view_logs.SelectedText = "";

            }

            // Dodaj nowy tekst
            int p1 = this.view_logs.TextLength;
            this.view_logs.SelectionStart = this.view_logs.TextLength;
            this.view_logs.SelectionColor = color;
            this.view_logs.SelectionIndent = indentation;
            this.view_logs.AppendText(msg + "\r\n");
            this.view_logs.SelectionColor = Color.White;
            this.view_logs.SelectionIndent = 0;

            // Przesuń na koniec, jeżeli użytkownik chce
            if (this.AutoScrol)
                this.view_logs.ScrollToCaret();
        }


        public void Clear()
        {
            this.view_logs.Clear();
        }
    }
}
