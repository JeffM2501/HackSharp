using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PewClient
{
    public partial class Register : Form
    {
        public string User = string.Empty;
        public string Pass = string.Empty;

        public string RootURL = "http://www.awesomelaser.com/gauth/?";

        public Register()
        {
            InitializeComponent();
            ErrorLabel.Text = string.Empty;
        }

        void CheckButtons()
        {
            ErrorLabel.Text = string.Empty;
            OK.Enabled = false;
            CheckName.Enabled = Callsign.Text != string.Empty;

            if (Password1.Text == string.Empty || Password2.Text == string.Empty)
                return;

            if (Password1.Text != Password2.Text)
            {
                ErrorLabel.Text = "Passwords do not match";
                return;
            }

            OK.Enabled = Email.Text != string.Empty && Terms.CheckState = CheckState.Checked;
        }

        private void Email_TextChanged(object sender, EventArgs e)
        {
            CheckButtons();
        }

        private void Password1_TextChanged(object sender, EventArgs e)
        {
            CheckButtons();
        }

        private void Password2_TextChanged(object sender, EventArgs e)
        {
            CheckButtons();
        }

        private void Callsign_TextChanged(object sender, EventArgs e)
        {
            CheckButtons();
        }

        private void CheckName_Click(object sender, EventArgs e)
        {
            // check the name
            WebClient client = new WebClient();
            string result = client.DownloadString(new Uri(RootURL + "action=check&name=" + Callsign.Text));
            if (result == "OK")
                MessageBox.Show("The name " + Callsign.Text + " is available");
            else
            {
                MessageBox.Show("The name " + Callsign.Text + " is taken, please choose another.");
                Callsign.Text = string.Empty;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            // do the registration
        }

        private void Terms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // show terms
        }
    }
}
