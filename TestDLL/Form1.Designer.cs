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
            button1_START = new System.Windows.Forms.Button();
            textBox_IP = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            textBox_port = new System.Windows.Forms.TextBox();
            button_changeIP = new System.Windows.Forms.Button();
            label5 = new System.Windows.Forms.Label();
            textBox_factorA = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            textBox_factorB = new System.Windows.Forms.TextBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            textBox_Y = new System.Windows.Forms.TextBox();
            textBox_Electrodes = new System.Windows.Forms.TextBox();
            button2 = new System.Windows.Forms.Button();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            startArBroadcast = new System.Windows.Forms.Button();
            arDeviceIp = new System.Windows.Forms.TextBox();
            arBroadcastPort = new System.Windows.Forms.Label();
            arBroadcastStatus = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            textBox1 = new System.Windows.Forms.TextBox();
            label14 = new System.Windows.Forms.Label();
            textBox_C = new System.Windows.Forms.TextBox();
            textBox_STD = new System.Windows.Forms.TextBox();
            textBox_AVG = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            button4_STOP = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            textBox_TimeStep = new System.Windows.Forms.TextBox();
            errorBox = new System.Windows.Forms.GroupBox();
            textBox_Error = new System.Windows.Forms.Label();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            errorBox.SuspendLayout();
            SuspendLayout();
            // 
            // button1_START
            // 
            button1_START.Location = new System.Drawing.Point(37, 53);
            button1_START.Margin = new System.Windows.Forms.Padding(2);
            button1_START.Name = "button1_START";
            button1_START.Size = new System.Drawing.Size(154, 30);
            button1_START.TabIndex = 13;
            button1_START.Text = "START";
            button1_START.UseVisualStyleBackColor = true;
            button1_START.Click += button1_Click;
            // 
            // textBox_IP
            // 
            textBox_IP.Location = new System.Drawing.Point(34, 37);
            textBox_IP.Margin = new System.Windows.Forms.Padding(2);
            textBox_IP.Name = "textBox_IP";
            textBox_IP.Size = new System.Drawing.Size(88, 23);
            textBox_IP.TabIndex = 0;
            textBox_IP.Text = "127.0.0.1";
            textBox_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox_IP.TextChanged += textBox_IP_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(14, 37);
            label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(17, 15);
            label2.TabIndex = 4;
            label2.Text = "IP";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(139, 37);
            label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(29, 15);
            label3.TabIndex = 5;
            label3.Text = "port";
            // 
            // textBox_port
            // 
            textBox_port.Location = new System.Drawing.Point(173, 37);
            textBox_port.Margin = new System.Windows.Forms.Padding(2);
            textBox_port.Name = "textBox_port";
            textBox_port.Size = new System.Drawing.Size(44, 23);
            textBox_port.TabIndex = 1;
            textBox_port.Text = "7777";
            textBox_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            textBox_port.TextChanged += textBox_port_TextChanged;
            // 
            // button_changeIP
            // 
            button_changeIP.Location = new System.Drawing.Point(227, 37);
            button_changeIP.Margin = new System.Windows.Forms.Padding(2);
            button_changeIP.Name = "button_changeIP";
            button_changeIP.Size = new System.Drawing.Size(97, 27);
            button_changeIP.TabIndex = 2;
            button_changeIP.Text = "Change IP";
            button_changeIP.UseVisualStyleBackColor = true;
            button_changeIP.Click += button_changeIP_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(13, 275);
            label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(29, 15);
            label5.TabIndex = 9;
            label5.Text = "A(x)";
            // 
            // textBox_factorA
            // 
            textBox_factorA.Location = new System.Drawing.Point(40, 25);
            textBox_factorA.Margin = new System.Windows.Forms.Padding(2);
            textBox_factorA.Name = "textBox_factorA";
            textBox_factorA.Size = new System.Drawing.Size(68, 23);
            textBox_factorA.TabIndex = 9;
            textBox_factorA.Text = "20.4";
            textBox_factorA.TextChanged += textBox_factorA_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(128, 271);
            label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(34, 15);
            label6.TabIndex = 11;
            label6.Text = "B( k )";
            // 
            // textBox_factorB
            // 
            textBox_factorB.Location = new System.Drawing.Point(161, 25);
            textBox_factorB.Margin = new System.Windows.Forms.Padding(2);
            textBox_factorB.Name = "textBox_factorB";
            textBox_factorB.Size = new System.Drawing.Size(68, 23);
            textBox_factorB.TabIndex = 10;
            textBox_factorB.Text = "16";
            textBox_factorB.TextChanged += textBox_factorB_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(10, 52);
            checkBox1.Margin = new System.Windows.Forms.Padding(2);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(209, 19);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Process normalized measurements";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // textBox_Y
            // 
            textBox_Y.Location = new System.Drawing.Point(190, 121);
            textBox_Y.Margin = new System.Windows.Forms.Padding(2);
            textBox_Y.Name = "textBox_Y";
            textBox_Y.ReadOnly = true;
            textBox_Y.Size = new System.Drawing.Size(102, 23);
            textBox_Y.TabIndex = 5;
            textBox_Y.TabStop = false;
            // 
            // textBox_Electrodes
            // 
            textBox_Electrodes.Location = new System.Drawing.Point(18, 186);
            textBox_Electrodes.Margin = new System.Windows.Forms.Padding(2);
            textBox_Electrodes.Name = "textBox_Electrodes";
            textBox_Electrodes.Size = new System.Drawing.Size(44, 23);
            textBox_Electrodes.TabIndex = 6;
            textBox_Electrodes.Text = "16";
            textBox_Electrodes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox_Electrodes.TextChanged += textBox_Electrodes_TextChanged;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(226, 181);
            button2.Margin = new System.Windows.Forms.Padding(2);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(97, 28);
            button2.TabIndex = 8;
            button2.Text = "Change electr.";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(134, 26);
            label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(29, 15);
            label11.TabIndex = 27;
            label11.Text = "port";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(13, 97);
            label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(17, 15);
            label12.TabIndex = 26;
            label12.Text = "IP";
            // 
            // groupBox1
            // 
            groupBox1.Location = new System.Drawing.Point(5, 12);
            groupBox1.Margin = new System.Windows.Forms.Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2);
            groupBox1.Size = new System.Drawing.Size(393, 58);
            groupBox1.TabIndex = 30;
            groupBox1.TabStop = false;
            groupBox1.Text = "TomoKISStudio connection";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(startArBroadcast);
            groupBox2.Controls.Add(arDeviceIp);
            groupBox2.Controls.Add(arBroadcastPort);
            groupBox2.Controls.Add(arBroadcastStatus);
            groupBox2.Controls.Add(label10);
            groupBox2.Controls.Add(label11);
            groupBox2.Location = new System.Drawing.Point(5, 70);
            groupBox2.Margin = new System.Windows.Forms.Padding(2);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(2);
            groupBox2.Size = new System.Drawing.Size(393, 87);
            groupBox2.TabIndex = 31;
            groupBox2.TabStop = false;
            groupBox2.Text = "AR app connection";
            // 
            // startArBroadcast
            // 
            startArBroadcast.Location = new System.Drawing.Point(222, 23);
            startArBroadcast.Name = "startArBroadcast";
            startArBroadcast.Size = new System.Drawing.Size(96, 26);
            startArBroadcast.TabIndex = 41;
            startArBroadcast.Text = "Broadcast";
            startArBroadcast.UseVisualStyleBackColor = true;
            startArBroadcast.Click += onStartArBroadcast;
            // 
            // arDeviceIp
            // 
            arDeviceIp.Location = new System.Drawing.Point(29, 23);
            arDeviceIp.Name = "arDeviceIp";
            arDeviceIp.Size = new System.Drawing.Size(100, 23);
            arDeviceIp.TabIndex = 40;
            // 
            // arBroadcastPort
            // 
            arBroadcastPort.AutoSize = true;
            arBroadcastPort.Location = new System.Drawing.Point(168, 27);
            arBroadcastPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            arBroadcastPort.Name = "arBroadcastPort";
            arBroadcastPort.Size = new System.Drawing.Size(31, 15);
            arBroadcastPort.TabIndex = 39;
            arBroadcastPort.Text = "8080";
            // 
            // arBroadcastStatus
            // 
            arBroadcastStatus.AutoSize = true;
            arBroadcastStatus.Location = new System.Drawing.Point(57, 52);
            arBroadcastStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            arBroadcastStatus.Name = "arBroadcastStatus";
            arBroadcastStatus.Size = new System.Drawing.Size(202, 15);
            arBroadcastStatus.TabIndex = 38;
            arBroadcastStatus.Text = "waiting for user to input AR device IP";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            label10.Location = new System.Drawing.Point(8, 52);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(42, 15);
            label10.TabIndex = 37;
            label10.Text = "Status:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(checkBox1);
            groupBox3.Location = new System.Drawing.Point(5, 162);
            groupBox3.Margin = new System.Windows.Forms.Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new System.Windows.Forms.Padding(2);
            groupBox3.Size = new System.Drawing.Size(393, 78);
            groupBox3.TabIndex = 32;
            groupBox3.TabStop = false;
            groupBox3.Text = "Number of electrodes in the sensor";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBox1);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(textBox_C);
            groupBox4.Controls.Add(textBox_factorA);
            groupBox4.Controls.Add(textBox_factorB);
            groupBox4.Controls.Add(textBox_STD);
            groupBox4.Controls.Add(textBox_AVG);
            groupBox4.Controls.Add(textBox_Y);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label1);
            groupBox4.Location = new System.Drawing.Point(5, 243);
            groupBox4.Margin = new System.Windows.Forms.Padding(2);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new System.Windows.Forms.Padding(2);
            groupBox4.Size = new System.Drawing.Size(391, 162);
            groupBox4.TabIndex = 33;
            groupBox4.TabStop = false;
            groupBox4.Text = "Factors for processing";
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(0, 0);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(116, 23);
            textBox1.TabIndex = 0;
            // 
            // label14
            // 
            label14.Location = new System.Drawing.Point(0, 0);
            label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(117, 27);
            label14.TabIndex = 1;
            // 
            // textBox_C
            // 
            textBox_C.Location = new System.Drawing.Point(296, 25);
            textBox_C.Margin = new System.Windows.Forms.Padding(2);
            textBox_C.Name = "textBox_C";
            textBox_C.Size = new System.Drawing.Size(88, 23);
            textBox_C.TabIndex = 11;
            textBox_C.Text = "1";
            textBox_C.TextChanged += textBox_C_TextChanged;
            // 
            // textBox_STD
            // 
            textBox_STD.Location = new System.Drawing.Point(282, 88);
            textBox_STD.Margin = new System.Windows.Forms.Padding(2);
            textBox_STD.Name = "textBox_STD";
            textBox_STD.ReadOnly = true;
            textBox_STD.Size = new System.Drawing.Size(102, 23);
            textBox_STD.TabIndex = 4;
            textBox_STD.TabStop = false;
            // 
            // textBox_AVG
            // 
            textBox_AVG.Location = new System.Drawing.Point(66, 88);
            textBox_AVG.Margin = new System.Windows.Forms.Padding(2);
            textBox_AVG.Name = "textBox_AVG";
            textBox_AVG.ReadOnly = true;
            textBox_AVG.Size = new System.Drawing.Size(102, 23);
            textBox_AVG.TabIndex = 3;
            textBox_AVG.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(194, 88);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(83, 15);
            label8.TabIndex = 20;
            label8.Text = "STD of RAW = ";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(9, 123);
            label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(170, 15);
            label13.TabIndex = 6;
            label13.Text = "Y =   C * X^2   +   A * X   +   B =";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(9, 88);
            label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(50, 15);
            label9.TabIndex = 5;
            label9.Text = "X = AVG";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(247, 28);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(46, 15);
            label1.TabIndex = 1;
            label1.Text = "C (x^2)";
            // 
            // button4_STOP
            // 
            button4_STOP.Enabled = false;
            button4_STOP.Location = new System.Drawing.Point(205, 53);
            button4_STOP.Margin = new System.Windows.Forms.Padding(2);
            button4_STOP.Name = "button4_STOP";
            button4_STOP.Size = new System.Drawing.Size(154, 30);
            button4_STOP.TabIndex = 14;
            button4_STOP.Text = "STOP";
            button4_STOP.UseVisualStyleBackColor = true;
            button4_STOP.Click += button4_STOP_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label7);
            groupBox5.Controls.Add(button4_STOP);
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(textBox_TimeStep);
            groupBox5.Controls.Add(button1_START);
            groupBox5.Location = new System.Drawing.Point(5, 410);
            groupBox5.Margin = new System.Windows.Forms.Padding(2);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new System.Windows.Forms.Padding(2);
            groupBox5.Size = new System.Drawing.Size(393, 95);
            groupBox5.TabIndex = 34;
            groupBox5.TabStop = false;
            groupBox5.Text = "Processing";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(218, 25);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(23, 15);
            label7.TabIndex = 2;
            label7.Text = "ms";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 25);
            label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(136, 15);
            label4.TabIndex = 1;
            label4.Text = "Loop's step time interval";
            // 
            // textBox_TimeStep
            // 
            textBox_TimeStep.Location = new System.Drawing.Point(152, 23);
            textBox_TimeStep.Margin = new System.Windows.Forms.Padding(2);
            textBox_TimeStep.Name = "textBox_TimeStep";
            textBox_TimeStep.Size = new System.Drawing.Size(60, 23);
            textBox_TimeStep.TabIndex = 12;
            textBox_TimeStep.Text = "300";
            textBox_TimeStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            textBox_TimeStep.TextChanged += textBox_TimeStep_TextChanged;
            // 
            // errorBox
            // 
            errorBox.Controls.Add(textBox_Error);
            errorBox.Location = new System.Drawing.Point(8, 510);
            errorBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            errorBox.Name = "errorBox";
            errorBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            errorBox.Size = new System.Drawing.Size(233, 117);
            errorBox.TabIndex = 37;
            errorBox.TabStop = false;
            errorBox.Text = "Last error";
            // 
            // textBox_Error
            // 
            textBox_Error.AutoSize = true;
            textBox_Error.Location = new System.Drawing.Point(10, 23);
            textBox_Error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            textBox_Error.Name = "textBox_Error";
            textBox_Error.Size = new System.Drawing.Size(0, 15);
            textBox_Error.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(402, 737);
            Controls.Add(errorBox);
            Controls.Add(groupBox5);
            Controls.Add(label12);
            Controls.Add(button2);
            Controls.Add(textBox_Electrodes);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button_changeIP);
            Controls.Add(textBox_port);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox_IP);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            Controls.Add(groupBox4);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "Form1";
            Text = "DLL tester";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            errorBox.ResumeLayout(false);
            errorBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.GroupBox errorBox;
        private System.Windows.Forms.Label textBox_Error;
        private System.Windows.Forms.Label arBroadcastStatus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label arBroadcastPort;
        private System.Windows.Forms.TextBox arDeviceIp;
        private System.Windows.Forms.Button startArBroadcast;
    }
}

