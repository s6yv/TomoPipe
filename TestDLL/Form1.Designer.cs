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
            this.button1_START = new System.Windows.Forms.Button();
            this.textBox_IP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_changeIP = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_factorA = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_factorB = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.textBox_Electrodes = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.arAppSocketStatus = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox_C = new System.Windows.Forms.TextBox();
            this.textBox_STD = new System.Windows.Forms.TextBox();
            this.textBox_AVG = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button4_STOP = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_TimeStep = new System.Windows.Forms.TextBox();
            this.arServerIp = new System.Windows.Forms.Label();
            this.errorBox = new System.Windows.Forms.GroupBox();
            this.textBox_Error = new System.Windows.Forms.Label();
            this.arServerPort = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.errorBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1_START
            // 
            this.button1_START.Location = new System.Drawing.Point(32, 46);
            this.button1_START.Margin = new System.Windows.Forms.Padding(2);
            this.button1_START.Name = "button1_START";
            this.button1_START.Size = new System.Drawing.Size(132, 26);
            this.button1_START.TabIndex = 13;
            this.button1_START.Text = "START";
            this.button1_START.UseVisualStyleBackColor = true;
            this.button1_START.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(29, 32);
            this.textBox_IP.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(76, 20);
            this.textBox_IP.TabIndex = 0;
            this.textBox_IP.Text = "127.0.0.1";
            this.textBox_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "port";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(148, 32);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(38, 20);
            this.textBox_port.TabIndex = 1;
            this.textBox_port.Text = "7777";
            this.textBox_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox_port_TextChanged);
            // 
            // button_changeIP
            // 
            this.button_changeIP.Location = new System.Drawing.Point(195, 32);
            this.button_changeIP.Margin = new System.Windows.Forms.Padding(2);
            this.button_changeIP.Name = "button_changeIP";
            this.button_changeIP.Size = new System.Drawing.Size(83, 23);
            this.button_changeIP.TabIndex = 2;
            this.button_changeIP.Text = "Change IP";
            this.button_changeIP.UseVisualStyleBackColor = true;
            this.button_changeIP.Click += new System.EventHandler(this.button_changeIP_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 238);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "A(x)";
            // 
            // textBox_factorA
            // 
            this.textBox_factorA.Location = new System.Drawing.Point(34, 22);
            this.textBox_factorA.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_factorA.Name = "textBox_factorA";
            this.textBox_factorA.Size = new System.Drawing.Size(59, 20);
            this.textBox_factorA.TabIndex = 9;
            this.textBox_factorA.Text = "20.4";
            this.textBox_factorA.TextChanged += new System.EventHandler(this.textBox_factorA_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(110, 235);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "B( k )";
            // 
            // textBox_factorB
            // 
            this.textBox_factorB.Location = new System.Drawing.Point(138, 22);
            this.textBox_factorB.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_factorB.Name = "textBox_factorB";
            this.textBox_factorB.Size = new System.Drawing.Size(59, 20);
            this.textBox_factorB.TabIndex = 10;
            this.textBox_factorB.Text = "16";
            this.textBox_factorB.TextChanged += new System.EventHandler(this.textBox_factorB_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 45);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(188, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "Process normalized measurements";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(163, 105);
            this.textBox_Y.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.ReadOnly = true;
            this.textBox_Y.Size = new System.Drawing.Size(88, 20);
            this.textBox_Y.TabIndex = 5;
            this.textBox_Y.TabStop = false;
            // 
            // textBox_Electrodes
            // 
            this.textBox_Electrodes.Location = new System.Drawing.Point(15, 161);
            this.textBox_Electrodes.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_Electrodes.Name = "textBox_Electrodes";
            this.textBox_Electrodes.Size = new System.Drawing.Size(38, 20);
            this.textBox_Electrodes.TabIndex = 6;
            this.textBox_Electrodes.Text = "16";
            this.textBox_Electrodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_Electrodes.TextChanged += new System.EventHandler(this.textBox_Electrodes_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(194, 157);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 24);
            this.button2.TabIndex = 8;
            this.button2.Text = "Change electr.";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(115, 23);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 84);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "IP";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(4, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(337, 50);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TomoKISStudio connection";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.arServerPort);
            this.groupBox2.Controls.Add(this.arServerIp);
            this.groupBox2.Controls.Add(this.arAppSocketStatus);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Location = new System.Drawing.Point(4, 61);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(337, 75);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "AR app connection";
            // 
            // arAppSocketStatus
            // 
            this.arAppSocketStatus.AutoSize = true;
            this.arAppSocketStatus.Location = new System.Drawing.Point(49, 45);
            this.arAppSocketStatus.Name = "arAppSocketStatus";
            this.arAppSocketStatus.Size = new System.Drawing.Size(148, 13);
            this.arAppSocketStatus.TabIndex = 38;
            this.arAppSocketStatus.Text = "waiting for AR app to connect";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(7, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 37;
            this.label10.Text = "Status:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(4, 140);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(337, 68);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Number of electrodes in the sensor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.textBox_C);
            this.groupBox4.Controls.Add(this.textBox_factorA);
            this.groupBox4.Controls.Add(this.textBox_factorB);
            this.groupBox4.Controls.Add(this.textBox_STD);
            this.groupBox4.Controls.Add(this.textBox_AVG);
            this.groupBox4.Controls.Add(this.textBox_Y);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(4, 211);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(335, 140);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Factors for processing";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 23);
            this.label14.TabIndex = 1;
            // 
            // textBox_C
            // 
            this.textBox_C.Location = new System.Drawing.Point(254, 22);
            this.textBox_C.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_C.Name = "textBox_C";
            this.textBox_C.Size = new System.Drawing.Size(76, 20);
            this.textBox_C.TabIndex = 11;
            this.textBox_C.Text = "1";
            this.textBox_C.TextChanged += new System.EventHandler(this.textBox_C_TextChanged);
            // 
            // textBox_STD
            // 
            this.textBox_STD.Location = new System.Drawing.Point(242, 76);
            this.textBox_STD.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_STD.Name = "textBox_STD";
            this.textBox_STD.ReadOnly = true;
            this.textBox_STD.Size = new System.Drawing.Size(88, 20);
            this.textBox_STD.TabIndex = 4;
            this.textBox_STD.TabStop = false;
            // 
            // textBox_AVG
            // 
            this.textBox_AVG.Location = new System.Drawing.Point(57, 76);
            this.textBox_AVG.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_AVG.Name = "textBox_AVG";
            this.textBox_AVG.ReadOnly = true;
            this.textBox_AVG.Size = new System.Drawing.Size(88, 20);
            this.textBox_AVG.TabIndex = 3;
            this.textBox_AVG.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(166, 76);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "STD of RAW = ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 107);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Y =   C * X^2   +   A * X   +   B =";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 76);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "X = AVG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "C (x^2)";
            // 
            // button4_STOP
            // 
            this.button4_STOP.Enabled = false;
            this.button4_STOP.Location = new System.Drawing.Point(176, 46);
            this.button4_STOP.Margin = new System.Windows.Forms.Padding(2);
            this.button4_STOP.Name = "button4_STOP";
            this.button4_STOP.Size = new System.Drawing.Size(132, 26);
            this.button4_STOP.TabIndex = 14;
            this.button4_STOP.Text = "STOP";
            this.button4_STOP.UseVisualStyleBackColor = true;
            this.button4_STOP.Click += new System.EventHandler(this.button4_STOP_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.button4_STOP);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.textBox_TimeStep);
            this.groupBox5.Controls.Add(this.button1_START);
            this.groupBox5.Location = new System.Drawing.Point(4, 355);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(337, 82);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Processing";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(187, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Loop\'s step time interval";
            // 
            // textBox_TimeStep
            // 
            this.textBox_TimeStep.Location = new System.Drawing.Point(130, 20);
            this.textBox_TimeStep.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_TimeStep.Name = "textBox_TimeStep";
            this.textBox_TimeStep.Size = new System.Drawing.Size(52, 20);
            this.textBox_TimeStep.TabIndex = 12;
            this.textBox_TimeStep.Text = "300";
            this.textBox_TimeStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_TimeStep.TextChanged += new System.EventHandler(this.textBox_TimeStep_TextChanged);
            // 
            // arServerIp
            // 
            this.arServerIp.AutoSize = true;
            this.arServerIp.Location = new System.Drawing.Point(22, 24);
            this.arServerIp.Name = "arServerIp";
            this.arServerIp.Size = new System.Drawing.Size(64, 13);
            this.arServerIp.TabIndex = 0;
            this.arServerIp.Text = "192.168.1.0";
            // 
            // errorBox
            // 
            this.errorBox.Controls.Add(this.textBox_Error);
            this.errorBox.Location = new System.Drawing.Point(7, 442);
            this.errorBox.Name = "errorBox";
            this.errorBox.Size = new System.Drawing.Size(200, 101);
            this.errorBox.TabIndex = 37;
            this.errorBox.TabStop = false;
            this.errorBox.Text = "Last error";
            // 
            // textBox_Error
            // 
            this.textBox_Error.AutoSize = true;
            this.textBox_Error.Location = new System.Drawing.Point(9, 20);
            this.textBox_Error.Name = "textBox_Error";
            this.textBox_Error.Size = new System.Drawing.Size(0, 13);
            this.textBox_Error.TabIndex = 0;
            // 
            // arServerPort
            // 
            this.arServerPort.AutoSize = true;
            this.arServerPort.Location = new System.Drawing.Point(144, 24);
            this.arServerPort.Name = "arServerPort";
            this.arServerPort.Size = new System.Drawing.Size(31, 13);
            this.arServerPort.TabIndex = 39;
            this.arServerPort.Text = "8080";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 639);
            this.Controls.Add(this.errorBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_Electrodes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_changeIP);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "DLL tester";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.errorBox.ResumeLayout(false);
            this.errorBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1_START;
        private System.Windows.Forms.TextBox textBox_IP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_changeIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_factorA;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_factorB;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.TextBox textBox_Electrodes;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_C;
        private System.Windows.Forms.Button button4_STOP;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_TimeStep;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_STD;
        private System.Windows.Forms.TextBox textBox_AVG;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label arServerIp;
        private System.Windows.Forms.GroupBox errorBox;
        private System.Windows.Forms.Label textBox_Error;
        private System.Windows.Forms.Label arAppSocketStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label arServerPort;
    }
}

