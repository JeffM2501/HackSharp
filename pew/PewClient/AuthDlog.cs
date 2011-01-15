using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Messages;

namespace PewClient
{
    public partial class AuthDlog : Form
    {
        public string User = string.Empty;
        public string Pass = string.Empty;
        public UInt64 Token = 0;
        public UInt64 ID = 0;

        Stopwatch timeout;

        public AuthDlog()
        {
            InitializeComponent();
        }

        Timer timer;
        WebClient web;

        private void AuthDlog_Load(object sender, EventArgs e)
        {
            AuthMessage.Text = "Connecting to authentication server";

            web = new WebClient();
            web.DownloadStringCompleted +=new DownloadStringCompletedEventHandler(web_DownloadStringCompleted);
            Uri url = new Uri(RootHost.ListURL + "action=auth&email=" + User + "&pass=" + Pass);
            web.DownloadStringAsync(url);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            timeout = new Stopwatch();
            timeout.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            progressBar1.Update();

            if (timeout.ElapsedMilliseconds > 60 * 1000)
            {
                AuthMessage.Text = "Server Timeout";
                timer.Stop();
                web.CancelAsync();
                Token = 0;
                DialogResult = DialogResult.Cancel;
            }
        }

        void web_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            timer.Stop();

            string result = e.Result;
            if (result == "BAD")
            {
                Token = 0;
                DialogResult = DialogResult.Cancel;
            }

            string[] nugs = result.Split(" ".ToCharArray());
            if (nugs.Length > 2 && nugs[0] == "OK")
            {
                ID = UInt64.Parse(nugs[1]);
                Token = UInt64.Parse(nugs[2]);
                DialogResult = DialogResult.OK;
            }
            else
            {
                Token = 0;
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
