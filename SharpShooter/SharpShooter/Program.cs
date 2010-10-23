using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SharpShooter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form = new Form1();
            App app = new App(form);
            Application.Run(form);
        }
    }
}
