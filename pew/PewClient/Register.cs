using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using Messages;

namespace PewClient
{
    public partial class Register : Form
    {
        public string User = string.Empty;
        public string Pass = string.Empty;

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

            OK.Enabled = Email.Text != string.Empty && Agree.Checked;
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
            string result = client.DownloadString(new Uri(RootHost.AuthURL + "action=check&name=" + Callsign.Text));
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
            WebClient client = new WebClient();
            string url = RootHost.AuthURL + "action=add&name=" + Callsign.Text;
            url += "email=" + Email.Text;
            url += "pass1=" + Password1.Text;
            url += "pass2=" + Password2.Text;

            string result = client.DownloadString(new Uri(url));
            if (result != "OK")
            {
                string[] nugs = result.Split(" ".ToCharArray());

                string error = string.Empty;
                if (nugs.Length > 1)
                {
                    if (nugs[1] == "NAME")
                    {
                        error = "The name " + Callsign.Text + " is not available.";
                        Callsign.Text = string.Empty;
                    }
                    else if (nugs[1] == "EMAIL")
                        error = "An account for " + Email.Text + " already exists.";
                    else if (nugs[1] == "PASS")
                        error = "Passwords do not match.";
                    else
                        error = "The registration process did not complete.";
                }
                else
                    error = "The registration process did not complete.";

                MessageBox.Show(error);

                DialogResult = DialogResult.None;
            }
            else
            {
                User = Email.Text;
                Pass = Password1.Text;
            }
        }

        private void Terms_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // show terms
            Process.Start(RootHost.AuthURL + "action=terms");
        }
    }
}
