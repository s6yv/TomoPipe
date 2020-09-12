namespace PlatomMonitor.Windows
{
    partial class MainWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.button1 = new System.Windows.Forms.Button();
            this.button_show_monitor = new System.Windows.Forms.Button();
            this.button_run_validators = new System.Windows.Forms.Button();
            this.button_info = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(32, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(409, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Dodaj monitor konsolowy";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button_show_monitor
            // 
            this.button_show_monitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_show_monitor.Location = new System.Drawing.Point(32, 96);
            this.button_show_monitor.Name = "button_show_monitor";
            this.button_show_monitor.Size = new System.Drawing.Size(409, 34);
            this.button_show_monitor.TabIndex = 1;
            this.button_show_monitor.Text = "Monitor węzła sieciowego Platom";
            this.button_show_monitor.UseVisualStyleBackColor = true;
            this.button_show_monitor.Click += new System.EventHandler(this.button_show_monitor_Click);
            // 
            // button_run_validators
            // 
            this.button_run_validators.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_run_validators.Location = new System.Drawing.Point(32, 158);
            this.button_run_validators.Name = "button_run_validators";
            this.button_run_validators.Size = new System.Drawing.Size(409, 34);
            this.button_run_validators.TabIndex = 2;
            this.button_run_validators.Text = "Walidacja schematu walidacyjnego i komunikatu";
            this.button_run_validators.UseVisualStyleBackColor = true;
            this.button_run_validators.Click += new System.EventHandler(this.button_run_validators_Click);
            // 
            // button_info
            // 
            this.button_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_info.AutoSize = true;
            this.button_info.Location = new System.Drawing.Point(439, 210);
            this.button_info.Name = "button_info";
            this.button_info.Size = new System.Drawing.Size(25, 13);
            this.button_info.TabIndex = 3;
            this.button_info.TabStop = true;
            this.button_info.Text = "Info";
            this.button_info.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.button_info_Clicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 232);
            this.Controls.Add(this.button_info);
            this.Controls.Add(this.button_run_validators);
            this.Controls.Add(this.button_show_monitor);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Platom Network Toolbox";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_show_monitor;
        private System.Windows.Forms.Button button_run_validators;
        private System.Windows.Forms.LinkLabel button_info;
    }
}

