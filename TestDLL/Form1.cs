using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RocsoleDataConverter;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        Converter obj;
        int stopThread = 0;
        int timeInteval = 350;

        public Form1()
        {
            InitializeComponent();
            obj = new Converter();

            textBox_IP.Text = obj.TomoKISStudioIP;
            textBox_port.Text = ""+obj.TomoKISStudioPort;
            textBox_UDPIP.Text = obj.UDPIP;
            textBox_UDPPort.Text = ""+obj.UDPPort;
            textBox_Electrodes.Text = ""+obj.ElectrodesCount;
            textBox_factorA.Text = ""+obj.FactorA;
            textBox_factorB.Text = ""+obj.FactorB;
            textBox_C.Text = "" + obj.FactorC;
            textBox_Electrodes.Text = "" + obj.ElectrodesCount;
            checkBox1.Checked = obj.ConsiderNormalizedData;

            timeInteval = obj.TimeInterval;
            textBox_TimeStep.Text = "" + timeInteval;
        }

        public void mainThread()
        {
            while (stopThread == 0)
            {
                double y = obj.ProcessNextFrame();
                textBox_Y.Invoke((MethodInvoker)delegate { textBox_Y.Text = "Y = " + y.ToString("0.#############"); });
                //textBox_Y.Text = "Y = " + y.ToString("0.#############");
                Thread.Sleep(timeInteval);
            }
            button1_START.Invoke((MethodInvoker)delegate { button1_START.Enabled = true; });
            button4_STOP.Invoke((MethodInvoker)delegate { button4_STOP.Enabled = false; });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_IP.Enabled = false;
            textBox_port.Enabled = false;
            textBox_UDPIP.Enabled = false;
            textBox_UDPPort.Enabled = false;
            textBox_Electrodes.Enabled = false;
            button1_START.Enabled = false;
            button4_STOP.Enabled = true;
            stopThread = 0;
            ThreadStart childref = new ThreadStart(mainThread);
            Thread mainTh = new Thread(childref);
            mainTh.Start();
        }

        private void button4_STOP_Click(object sender, EventArgs e)
        {
            stopThread = 1;
            button4_STOP.Enabled = false;
        }

        private void button_changeIP_Click(object sender, EventArgs e)
        {
            stopThread = 1;
            textBox_IP.Enabled = true;
            textBox_port.Enabled = true;
            obj.CloseTomoKISStudioConnection();            
        }

        private void textBox_factorA_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                obj.FactorA = Double.Parse(textBox_factorA.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void textBox_factorB_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                obj.FactorB = Double.Parse(textBox_factorB.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            obj.ConsiderNormalizedData = checkBox1.Checked;
        }

        private void textBox_Electrodes_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try { 
                obj.ElectrodesCount = Int32.Parse(textBox_Electrodes.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopThread = 1;
            textBox_Electrodes.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stopThread = 1;
            textBox_UDPIP.Enabled = true;
            textBox_UDPPort.Enabled = true;
        }

        private void textBox_IP_TextChanged(object sender, EventArgs e)
        {
            obj.TomoKISStudioIP = textBox_IP.Text; ;
        }

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                obj.TomoKISStudioPort = Int32.Parse(textBox_port.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void textBox_UDPIP_TextChanged(object sender, EventArgs e)
        {
            obj.UDPIP = textBox_UDPIP.Text;
        }

        private void textBox_UDPPort_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                obj.UDPPort = Int32.Parse(textBox_UDPPort.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void textBox_C_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                obj.FactorC = Double.Parse(textBox_C.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void textBox_TimeStep_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int tmp = int.Parse(textBox_TimeStep.Text);
                if (tmp <= 0)
                    throw new Exception("the time interval must be greater then 0ms");
                timeInteval = tmp;
                Console.WriteLine("New timeInterval = " + timeInteval + "ms");
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
                textBox_TimeStep.Text = "" + timeInteval;
            }
        }
    }
}
