namespace SimpleLogger
{
    partial class LoggerView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.view_logs = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // view_logs
            // 
            this.view_logs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.view_logs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.view_logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.view_logs.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.view_logs.ForeColor = System.Drawing.Color.White;
            this.view_logs.Location = new System.Drawing.Point(0, 0);
            this.view_logs.Name = "view_logs";
            this.view_logs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.view_logs.Size = new System.Drawing.Size(282, 145);
            this.view_logs.TabIndex = 4;
            this.view_logs.Text = "";
            // 
            // LoggerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.view_logs);
            this.Name = "LoggerView";
            this.Size = new System.Drawing.Size(282, 145);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox view_logs;
    }
}
