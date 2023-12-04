using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDLL
{
    public partial class GasCoreView : Form
    {

        private const int PIPE_DIAMETER = 120; //mm
        private const int PIPE_PIXEL_DIAMETER = 600; //px
        private const int PIPE_CENTER = 5+ PIPE_PIXEL_DIAMETER / 2; //px

        private Graphics g;
        private Pen p;

        public GasCoreView()
        {
            InitializeComponent();
        }

        private void GasCoreView_Load(object sender, EventArgs e)
        {
            this.Size = new Size(PIPE_PIXEL_DIAMETER + 25, PIPE_PIXEL_DIAMETER + 50);
            g = CreateGraphics();
            p = new Pen(Color.Black, 3);
        }

        internal void drawAll(double coreDiameter, double coreDistanceFromCenter, double angle)
        {
            g.Clear(this.BackColor);
            drawPipe();
            drawGasCore(coreDiameter, coreDistanceFromCenter, angle);
        }

        private void drawPipe()
        {
            p.Color = Color.Black;
            g.DrawEllipse(p, PIPE_CENTER - PIPE_PIXEL_DIAMETER / 2, PIPE_CENTER - PIPE_PIXEL_DIAMETER / 2 , PIPE_PIXEL_DIAMETER, PIPE_PIXEL_DIAMETER);
            g.DrawLine(p, new Point(PIPE_CENTER, PIPE_CENTER + 10), new Point(PIPE_CENTER, PIPE_CENTER - 10));
            g.DrawLine(p, new Point(PIPE_CENTER + 10, PIPE_CENTER), new Point(PIPE_CENTER - 10, PIPE_CENTER));
        }

        private void drawGasCore(double coreDiameter, double coreDistanceFromCenter, double angle)
        {
            double radians = Math.PI * angle / 180.0;
            p.Color = Color.Red;
            int corePixelDiameter = (int)((coreDiameter / (double)PIPE_DIAMETER) * PIPE_PIXEL_DIAMETER);
            int x_coreCenter = (int)(PIPE_CENTER + Math.Cos(radians) * ((coreDistanceFromCenter / (double)PIPE_DIAMETER) * PIPE_PIXEL_DIAMETER));
            int y_coreCenter = (int)(PIPE_CENTER + Math.Sin(radians) * ((coreDistanceFromCenter / (double)PIPE_DIAMETER) * PIPE_PIXEL_DIAMETER));
            g.DrawEllipse(p, x_coreCenter - corePixelDiameter / 2, y_coreCenter - corePixelDiameter / 2, corePixelDiameter, corePixelDiameter);
        }

        private void GasCoreView_Paint(object sender, PaintEventArgs e)
        {
            drawPipe();
        }
    }
}
