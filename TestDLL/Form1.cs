using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using RocsoleDataConverter;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        int stopThread = 0;
        int timeInteval = 350;
        Converter converter = new Converter();

        public Form1()
        {
            InitializeComponent();
            
            textBox_IP.Text = converter.TomoKISStudioIP;
            textBox_port.Text = ""+converter.TomoKISStudioPort;
            arServerIp.Text = converter.arAppConnection.ipAddress;
            arServerPort.Text = ""+converter.arAppConnection.port;
            textBox_Electrodes.Text = ""+converter.ElectrodesCount;
            textBox_factorA.Text = ""+converter.FactorA;
            textBox_factorB.Text = ""+converter.FactorB;
            textBox_C.Text = "" + converter.FactorC;
            textBox1.Text = "" + converter.Avgf;
            textBox_Electrodes.Text = "" + converter.ElectrodesCount;
            checkBox1.Checked = converter.ConsiderNormalizedData;

            timeInteval = converter.TimeInterval;
            textBox_TimeStep.Text = "" + timeInteval;
        }

        public void mainThread()
        {
            while (stopThread == 0)
            {
                bool res = converter.ProcessNextFrame(out double avg, out double std, out double Y);
                if (!res)
                {
                    textBox_Y.Invoke((MethodInvoker)delegate { textBox_Y.Text = "Error"; });
                    textBox_AVG.Invoke((MethodInvoker)delegate { textBox_AVG.Text = "Error"; });
                    textBox_STD.Invoke((MethodInvoker)delegate { textBox_STD.Text = "Error"; });
                }
                else
                {
                    textBox_Y.Invoke((MethodInvoker)delegate { textBox_Y.Text = Y.ToString("0.#############"); });
                    textBox_AVG.Invoke((MethodInvoker)delegate { textBox_AVG.Text = avg.ToString("0.#############"); });
                    textBox_STD.Invoke((MethodInvoker)delegate { textBox_STD.Text = std.ToString("0.#############"); });
                }
                Thread.Sleep(timeInteval);
            }
            button1_START.Invoke((MethodInvoker)delegate { button1_START.Enabled = true; });
            button4_STOP.Invoke((MethodInvoker)delegate { button4_STOP.Enabled = false; });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_IP.Enabled = false;
            textBox_port.Enabled = false;
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
            converter.CloseTomoKISStudioConnection();            
        }

        private void textBox_factorA_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                converter.FactorA = Double.Parse(textBox_factorA.Text);
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
                converter.FactorB = Double.Parse(textBox_factorB.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            converter.ConsiderNormalizedData = checkBox1.Checked;
        }

        private void textBox_Electrodes_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try { 
                converter.ElectrodesCount = Int32.Parse(textBox_Electrodes.Text);
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
        }

        private void textBox_IP_TextChanged(object sender, EventArgs e)
        {
            converter.TomoKISStudioIP = textBox_IP.Text;
        }

        private void textBox_port_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                converter.TomoKISStudioPort = Int32.Parse(textBox_port.Text);
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
                converter.FactorC = Double.Parse(textBox_C.Text);
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox_Error.Text = "";
            try
            {
                converter.Avgf = Double.Parse(textBox1.Text);
            }
            catch (Exception ex)
            {
                textBox_Error.Text = ex.Message;
            }
        }
    }
}
