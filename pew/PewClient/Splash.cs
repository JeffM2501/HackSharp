using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Messages;

namespace PewClient
{
    public partial class Splash : Form
    {
        Prefs UserPrefs;

        public Splash()
        {
            InitializeComponent();
            VersionLabel.Text = "V." + Version.Major.ToString() + "." + Version.Major.ToString() + "." + Version.Revision.ToString() + "-" + MessageVersion.Version.ToString();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            if (!Prefs.Exists())
                registerToolStripMenuItem_Click(this, EventArgs.Empty);
           
            UserPrefs = Prefs.Load();

            if (UserPrefs.UserName == string.Empty)
                userSettingsToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register registerDlog = new Register();
            if (registerDlog.ShowDialog(this) == DialogResult.OK)
            {
                Prefs prefs = Prefs.Load();
                prefs.UserName = registerDlog.User;
                prefs.Password = registerDlog.Pass;
                prefs.Savepassword = true;
                Prefs.Save(prefs);
            }
        }

        private void userSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
