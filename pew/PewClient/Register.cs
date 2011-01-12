using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PewClient
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            ErrorLabel.Text = string.Empty;
        }

        void CheckButtons()
        {
            ErrorLabel.Text = string.Empty;
            OK.Enabled = false;
            CheckName.Enabled = false;

            if (Password1.Text == string.Empty || Password2.Text == string.Empty)
                return;

            if (Password1.Text != Password2.Text)
            {
                ErrorLabel.Text = "Passwords do not match";
                return;
            }

            if (Email.Text == string.Empty || Callsign.Text == string.Empty)
                return;

            OK.Enabled = true;
            CheckName.Enabled = true;
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
