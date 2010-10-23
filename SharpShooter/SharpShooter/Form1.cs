using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using OpenTK;

namespace SharpShooter
{
    public partial class Form1 : Form
    {
        Timer FPSTimer = new Timer();
        Stopwatch Clock = new Stopwatch();

        double Now = 0;
        double Before = 0;

        bool Loaded = false;

        public delegate void NonTimeEvent ();
        public delegate void TimeEvent( double Now, double Delta );

        public event NonTimeEvent Load;
        public event TimeEvent Update;
        public event TimeEvent Draw;

        public GLControl Control
        {
            get { return this.glControl1; }
        }

        public Form1()
        {
            InitializeComponent();
            FPSTimer.Interval = 16;
            FPSTimer.Tick += new EventHandler(FPSTimer_Tick);
        }

        void FPSTimer_Tick(object sender, EventArgs e)
        {
            Think();
            glControl1.Invalidate();
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            Clock.Start();

            Before = Clock.ElapsedMilliseconds/1000.0;
            Now = Clock.ElapsedMilliseconds / 1000.0;
            Loaded = true;
            if (Load != null)
                Load();
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (!Loaded)
                return;

            if (Draw != null)
                Draw(Now, Now - Before);

            glControl1.SwapBuffers();
        }

        protected void Think()
        {
            Before = Now;
            Now = Clock.ElapsedMilliseconds / 1000.0;

            if (Update != null)
                Update(Now, Now - Before);
        }
    }
}
