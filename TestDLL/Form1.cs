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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double y = obj.ProcessNextFrame();
            label4Y.Text = y.ToString("0.##");
        }
    }
}
