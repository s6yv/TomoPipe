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
            this.label4Y = new System.Windows.Forms.Label();
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
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_Error = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox_UDPPort = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_UDPIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_C = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button4_STOP = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_TimeStep = new System.Windows.Forms.TextBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1_START
            // 
            this.button1_START.Location = new System.Drawing.Point(42, 56);
            this.button1_START.Name = "button1_START";
            this.button1_START.Size = new System.Drawing.Size(176, 32);
            this.button1_START.TabIndex = 0;
            this.button1_START.Text = "START";
            this.button1_START.UseVisualStyleBackColor = true;
            this.button1_START.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4Y
            // 
            this.label4Y.AutoSize = true;
            this.label4Y.Location = new System.Drawing.Point(11, 415);
            this.label4Y.Name = "label4Y";
            this.label4Y.Size = new System.Drawing.Size(55, 17);
            this.label4Y.TabIndex = 1;
            this.label4Y.Text = "Y value";
            // 
            // textBox_IP
            // 
            this.textBox_IP.Location = new System.Drawing.Point(39, 39);
            this.textBox_IP.Name = "textBox_IP";
            this.textBox_IP.Size = new System.Drawing.Size(100, 22);
            this.textBox_IP.TabIndex = 2;
            this.textBox_IP.Text = "127.0.0.1";
            this.textBox_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_IP.TextChanged += new System.EventHandler(this.textBox_IP_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "port";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(198, 39);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(50, 22);
            this.textBox_port.TabIndex = 6;
            this.textBox_port.Text = "7777";
            this.textBox_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_port.TextChanged += new System.EventHandler(this.textBox_port_TextChanged);
            // 
            // button_changeIP
            // 
            this.button_changeIP.Location = new System.Drawing.Point(260, 39);
            this.button_changeIP.Name = "button_changeIP";
            this.button_changeIP.Size = new System.Drawing.Size(111, 28);
            this.button_changeIP.TabIndex = 7;
            this.button_changeIP.Text = "Change IP";
            this.button_changeIP.UseVisualStyleBackColor = true;
            this.button_changeIP.Click += new System.EventHandler(this.button_changeIP_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(177, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "A(x)";
            // 
            // textBox_factorA
            // 
            this.textBox_factorA.Location = new System.Drawing.Point(212, 23);
            this.textBox_factorA.Name = "textBox_factorA";
            this.textBox_factorA.Size = new System.Drawing.Size(77, 22);
            this.textBox_factorA.TabIndex = 10;
            this.textBox_factorA.Text = "20.4";
            this.textBox_factorA.TextChanged += new System.EventHandler(this.textBox_factorA_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(300, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "B(k)";
            // 
            // textBox_factorB
            // 
            this.textBox_factorB.Location = new System.Drawing.Point(334, 23);
            this.textBox_factorB.Name = "textBox_factorB";
            this.textBox_factorB.Size = new System.Drawing.Size(77, 22);
            this.textBox_factorB.TabIndex = 12;
            this.textBox_factorB.Text = "16";
            this.textBox_factorB.TextChanged += new System.EventHandler(this.textBox_factorB_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(251, 21);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Process normalized measurements";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox_Y
            // 
            this.textBox_Y.Location = new System.Drawing.Point(70, 415);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.ReadOnly = true;
            this.textBox_Y.Size = new System.Drawing.Size(177, 22);
            this.textBox_Y.TabIndex = 17;
            // 
            // textBox_Electrodes
            // 
            this.textBox_Electrodes.Location = new System.Drawing.Point(20, 170);
            this.textBox_Electrodes.Name = "textBox_Electrodes";
            this.textBox_Electrodes.Size = new System.Drawing.Size(50, 22);
            this.textBox_Electrodes.TabIndex = 20;
            this.textBox_Electrodes.Text = "16";
            this.textBox_Electrodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_Electrodes.TextChanged += new System.EventHandler(this.textBox_Electrodes_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(259, 165);
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
            this.label10.Location = new System.Drawing.Point(10, 444);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 17);
            this.label10.TabIndex = 22;
            this.label10.Text = "Last error";
            // 
            // textBox_Error
            // 
            this.textBox_Error.Location = new System.Drawing.Point(13, 465);
            this.textBox_Error.Multiline = true;
            this.textBox_Error.Name = "textBox_Error";
            this.textBox_Error.Size = new System.Drawing.Size(438, 105);
            this.textBox_Error.TabIndex = 23;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(259, 104);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 28);
            this.button3.TabIndex = 29;
            this.button3.Text = "Change IP";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox_UDPPort
            // 
            this.textBox_UDPPort.Location = new System.Drawing.Point(197, 104);
            this.textBox_UDPPort.Name = "textBox_UDPPort";
            this.textBox_UDPPort.Size = new System.Drawing.Size(50, 22);
            this.textBox_UDPPort.TabIndex = 28;
            this.textBox_UDPPort.Text = "777";
            this.textBox_UDPPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_UDPPort.TextChanged += new System.EventHandler(this.textBox_UDPPort_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(158, 104);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 17);
            this.label11.TabIndex = 27;
            this.label11.Text = "port";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 17);
            this.label12.TabIndex = 26;
            this.label12.Text = "IP";
            // 
            // textBox_UDPIP
            // 
            this.textBox_UDPIP.Location = new System.Drawing.Point(38, 104);
            this.textBox_UDPIP.Name = "textBox_UDPIP";
            this.textBox_UDPIP.Size = new System.Drawing.Size(100, 22);
            this.textBox_UDPIP.TabIndex = 24;
            this.textBox_UDPIP.Text = "127.0.0.1";
            this.textBox_UDPIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_UDPIP.TextChanged += new System.EventHandler(this.textBox_UDPIP_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(6, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(449, 62);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TomoKISStudio connection";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(6, 80);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(449, 57);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "UDP endpoint";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(6, 144);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(449, 83);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Number of electrodes in the sensor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_C);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.textBox_factorB);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.textBox_factorA);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(6, 231);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(447, 64);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Factors for processing";
            // 
            // textBox_C
            // 
            this.textBox_C.Location = new System.Drawing.Point(71, 23);
            this.textBox_C.Name = "textBox_C";
            this.textBox_C.Size = new System.Drawing.Size(100, 22);
            this.textBox_C.TabIndex = 2;
            this.textBox_C.Text = "1";
            this.textBox_C.TextChanged += new System.EventHandler(this.textBox_C_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "C (x^2)";
            // 
            // button4_STOP
            // 
            this.button4_STOP.Enabled = false;
            this.button4_STOP.Location = new System.Drawing.Point(234, 56);
            this.button4_STOP.Name = "button4_STOP";
            this.button4_STOP.Size = new System.Drawing.Size(176, 32);
            this.button4_STOP.TabIndex = 3;
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
            this.groupBox5.Location = new System.Drawing.Point(6, 301);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(449, 100);
            this.groupBox5.TabIndex = 34;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Processing";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(249, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Loop\'s step time interval";
            // 
            // textBox_TimeStep
            // 
            this.textBox_TimeStep.Location = new System.Drawing.Point(174, 24);
            this.textBox_TimeStep.Name = "textBox_TimeStep";
            this.textBox_TimeStep.Size = new System.Drawing.Size(68, 22);
            this.textBox_TimeStep.TabIndex = 0;
            this.textBox_TimeStep.Text = "300";
            this.textBox_TimeStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_TimeStep.TextChanged += new System.EventHandler(this.textBox_TimeStep_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 586);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox_UDPPort);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBox_UDPIP);
            this.Controls.Add(this.textBox_Error);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_Electrodes);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.button_changeIP);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_IP);
            this.Controls.Add(this.label4Y);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Name = "Form1";
            this.Text = "DLL tester";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1_START;
        private System.Windows.Forms.Label label4Y;
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_Error;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox_UDPPort;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_UDPIP;
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
    }
}

