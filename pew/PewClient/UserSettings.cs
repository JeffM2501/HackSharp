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
    public partial class UserSettings : Form
    {
        public string User = string.Empty;
        public string Pass = string.Empty;
        public bool Save = false;

        public UserSettings()
        {
            InitializeComponent();
            Email.Text = User;
            Password.Text = Pass;
            SavePassword.Checked = Save;
        }

        private void SavePassword_CheckedChanged(object sender, EventArgs e)
        {
            Save = SavePassword.Checked;

            if (!SavePassword.Checked)
                Password.Text = string.Empty;
            else
                Password.Text = Pass;
        }
    }
}
