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
        public string Host = string.Empty;
        public UInt64 ID = 0;
        public UInt64 Token = 0;

        public Splash()
        {
            InitializeComponent();
            VersionLabel.Text = "V." + Version.Major.ToString() + "." + Version.Major.ToString() + "." + Version.Revision.ToString() + "-" + MessageVersion.Version.ToString();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            if (!Prefs.Exists())
                registerToolStripMenuItem_Click(this, EventArgs.Empty);

            Prefs prefs = Prefs.Load();

            if (prefs.UserName == string.Empty)
                userSettingsToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        protected void CheckPlay()
        {
            Prefs prefs = Prefs.Load();
            Play.Enabled = prefs.UserName != string.Empty;
        }

        protected void GetServerList()
        {

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
            UserSettings settings = new UserSettings();
            Prefs prefs = Prefs.Load();
            settings.User = prefs.UserName;
            settings.Pass = prefs.Password;
            settings.Save = prefs.Savepassword;
            if (settings.ShowDialog(this) == DialogResult.OK)
            {
                prefs.UserName = settings.User;
                prefs.Password = settings.Pass;
                prefs.Savepassword = settings.Save;
                Prefs.Save(prefs);
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            AuthDlog auth = new AuthDlog();

            Prefs prefs = Prefs.Load();
            if (prefs.UserName == string.Empty)
                userSettingsToolStripMenuItem_Click(this,EventArgs.Empty);

            if (prefs.UserName == string.Empty)
                return;

            auth.User = prefs.UserName;

            if (!prefs.Savepassword || prefs.Password != string.Empty)
            {
                GetPass p = new GetPass();
                p.Pass = prefs.Password;
                if (p.ShowDialog(this) == DialogResult.OK)
                    auth.Pass = p.Pass;
                else
                    return;
            }
            else
                auth.Pass = prefs.Password;

            if (auth.User == string.Empty || auth.Pass == string.Empty)
                return;

            // send login;
            DialogResult result = auth.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            ID = auth.ID;
            Token = auth.Token;

            // do the server list

            Close();
        }
    }
}
