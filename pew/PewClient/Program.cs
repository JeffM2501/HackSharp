using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PewClient
{

    public static class Version
    {
        public static int Major = 0;
        public static int Minor = 0;
        public static int Revision = 0;
    }

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
            Application.Run(new Splash());
        }
    }
}
