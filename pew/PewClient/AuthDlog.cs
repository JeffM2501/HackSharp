using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PewClient
{
    public partial class AuthDlog : Form
    {
        public string User = string.Empty;
        public string Pass = string.Empty;
        public UInt64 Token = 0;

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
            Uri url = new Uri("hiost")
            web.DownloadStringAsync(new )
            timer = new Timer();
        }

        void web_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            timer.Stop();

        }
    }
}
