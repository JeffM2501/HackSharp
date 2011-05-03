using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Renderer;

namespace Characterizer
{
    public partial class Form1 : Form
    {
        Display display = null;
        Camera camera = null;

        Stopwatch timer = new Stopwatch();
        long LastTime = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            timer.Start();
            LastTime = timer.ElapsedMilliseconds;

            display = new Display(glControl1);
            camera = new Camera(display);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        protected void Draw()
        {
            long delta = timer.ElapsedMilliseconds - LastTime;
            LastTime = timer.ElapsedMilliseconds;

            display.Clear();
            camera.Execute();

            display.SwapBuffers();
        }
    }
}
