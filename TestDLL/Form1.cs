using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RocsoleDataConverter;

namespace TestDLL
{
    public partial class Form1 : Form
    {
        Converter obj;
        public Form1()
        {
            InitializeComponent();
            obj = new Converter();
            obj.TomoKISStudioIP = textBox_IP.Text;
            obj.TomoKISStudioPort = Int32.Parse(textBox_port.Text);
            obj.ElectrodesCount = Int32.Parse(textBox_Electrodes.Text);
            obj.FactorA = 20.4;
            obj.FactorB = 16;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox_IP.Enabled = false;
            textBox_port.Enabled = false;
            textBox_Electrodes.Enabled = false;

            double y = obj.ProcessNextFrame();
            textBox_Y.Text = "Y = " + y.ToString("0.##");
        }

        private void button_changeIP_Click(object sender, EventArgs e)
        {
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
            textBox_Electrodes.Enabled = true;
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
    }
}
