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
        }

        private void SavePassword_CheckedChanged(object sender, EventArgs e)
        {
            Save = SavePassword.Checked;

            if (!SavePassword.Checked)
                Password.Text = string.Empty;
            else
                Password.Text = Pass;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            User = Email.Text;
            Pass = Password.Text;
            Save = SavePassword.Checked;
            DialogResult = DialogResult.OK;
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            Email.Text = User;
            Password.Text = Pass;
            SavePassword.Checked = Save;
        }
    }
}
