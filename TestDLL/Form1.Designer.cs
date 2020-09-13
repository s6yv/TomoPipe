namespace TestDLL
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.label4Y = new System.Windows.Forms.Label();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_changeIP = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_factorA = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_factorB = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_FilteredIndexes = new System.Windows.Forms.TextBox();
            this.textBox_Avg = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_Electrodes = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_Error = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(176, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "processNextFrame";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4Y
            // 
            this.label4Y.AutoSize = true;
            this.label4Y.Location = new System.Drawing.Point(9, 352);
            this.label4Y.Name = "label4Y";
            this.label4Y.Size = new System.Drawing.Size(55, 17);
            this.label4Y.TabIndex = 1;
            this.label4Y.Text = "Y value";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(28, 41);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 22);
            this.textBox_IP.TabIndex = 2;
            this.textBox_IP.Text = "127.0.0.1";
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "TomoKISStudio connection";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "port";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(187, 41);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(50, 22);
            this.textBox_port.TabIndex = 6;
            this.textBox_port.Text = "7";
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox_port_TextChanged);
            // 
            // button_changeIP
            // 
            this.button_changeIP.Location = new System.Drawing.Point(249, 41);
            this.button_changeIP.Name = "button_changeIP";
            this.button_changeIP.Size = new System.Drawing.Size(111, 28);
            this.button_changeIP.TabIndex = 7;
            this.button_changeIP.Text = "Change IP";
            this.button_changeIP.UseVisualStyleBackColor = true;
            this.button_changeIP.Click += new System.EventHandler(this.button_changeIP_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Factors for processing";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "A";
            // 
            // textBox_factorA
            // 
            this.textBox_factorA.Location = new System.Drawing.Point(26, 160);
            this.textBox_factorA.Name = "textBox_factorA";
            this.textBox_factorA.Size = new System.Drawing.Size(77, 22);
            this.textBox_factorA.TabIndex = 10;
            this.textBox_factorA.Text = "20.4";
            this.textBox_factorA.TextChanged += new System.EventHandler(this.textBox_factorA_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(147, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "B";
            // 
            // textBox_factorB
            // 
            this.textBox_factorB.Location = new System.Drawing.Point(167, 160);
            this.textBox_factorB.Name = "textBox_factorB";
            this.textBox_factorB.Size = new System.Drawing.Size(77, 22);
            this.textBox_factorB.TabIndex = 12;
            this.textBox_factorB.Text = "16";
            this.textBox_factorB.TextChanged += new System.EventHandler(this.textBox_factorB_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(8, 189);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(251, 21);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Process normalized measurements";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Filtered indexes";
            // 
            // textBox_FilteredIndexes
            // 
            this.textBox_FilteredIndexes.Location = new System.Drawing.Point(118, 291);
            this.textBox_FilteredIndexes.Name = "textBox_FilteredIndexes";
            this.textBox_FilteredIndexes.ReadOnly = true;
            this.textBox_FilteredIndexes.Size = new System.Drawing.Size(335, 22);
            this.textBox_FilteredIndexes.TabIndex = 15;
            // 
            // textBox_Avg
            // 
            this.textBox_Avg.Location = new System.Drawing.Point(118, 319);
            this.textBox_Avg.Name = "textBox_Avg";
            this.textBox_Avg.ReadOnly = true;
            this.textBox_Avg.Size = new System.Drawing.Size(93, 22);
            this.textBox_Avg.TabIndex = 16;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(118, 347);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.ReadOnly = true;
            this.textBox_Y.Size = new System.Drawing.Size(93, 22);
            this.textBox_Y.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 323);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Average";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(230, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "Number of electrodes in the sensor";
            // 
            // textBox_Electrodes
            // 
            this.textBox_Electrodes.Location = new System.Drawing.Point(8, 98);
            this.textBox_Electrodes.Name = "textBox_Electrodes";
            this.textBox_Electrodes.Size = new System.Drawing.Size(79, 22);
            this.textBox_Electrodes.TabIndex = 20;
            this.textBox_Electrodes.Text = "16";
            this.textBox_Electrodes.TextChanged += new System.EventHandler(this.textBox_Electrodes_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(249, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 28);
            this.button2.TabIndex = 21;
            this.button2.Text = "Change electr.";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 391);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "Last error";
            // 
            // textBox_Error
            // 
            this.textBox_Error.Location = new System.Drawing.Point(13, 412);
            this.textBox_Error.Multiline = true;
            this.textBox_Error.Name = "textBox_Error";
            this.textBox_Error.Size = new System.Drawing.Size(438, 105);
            this.textBox_Error.TabIndex = 23;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 529);
            this.Controls.Add(this.textBox_Error);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_Electrodes);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_Avg);
            this.Controls.Add(this.textBox_FilteredIndexes);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox_factorB);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_factorA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button_changeIP);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label4Y);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "DLL tester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4Y;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_changeIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_factorA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_factorB;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_FilteredIndexes;
        private System.Windows.Forms.TextBox textBox_Avg;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_Electrodes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_Error;
    }
}

