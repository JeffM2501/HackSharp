using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
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

        protected string Email = string.Empty;
        protected string Password = string.Empty;

        protected WebClient ListClient = new WebClient();

        protected bool ListInProgress = false;

        public class ServerListItem
        {
            public string Name = string.Empty;
            public string Host = string.Empty;
            public bool Open = false;

            public ServerListItem( string line )
            {
                string[] nugs = line.Split(";".ToCharArray());
                if (nugs.Length > 0)
                    Name = Host = nugs[0];
                if (nugs.Length > 1)
                    Name = nugs[1];
                if (nugs.Length > 2)
                    Open = nugs[2] == "OPEN";
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public Splash()
        {
            InitializeComponent();
            VersionLabel.Text = "V." + Version.Major.ToString() + "." + Version.Major.ToString() + "." + Version.Revision.ToString() + "-" + MessageVersion.Version.ToString();
            ListClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(ListClient_DownloadStringCompleted);
        }

        private void Splash_Load(object sender, EventArgs e)
        {
          
        }

        protected void CheckPlay()
        {
            Prefs prefs = Prefs.Load();
            Play.Enabled = prefs.UserName != string.Empty;
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

                Email = registerDlog.User;
                Password = registerDlog.Pass;

                StartListServerLookup();
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

                Email = settings.User;
                Password = settings.Pass;

                StartListServerLookup();
            }
        }

        protected bool Authenticate()
        {
            AuthDlog auth = new AuthDlog();

            if (Email != string.Empty && Password != string.Empty)
            {
                auth.User = Email;
                auth.Pass = string.Empty;
            }
            else
            {
                Prefs prefs = Prefs.Load();
                if (prefs.UserName == string.Empty)
                    userSettingsToolStripMenuItem_Click(this, EventArgs.Empty);

                if (prefs.UserName == string.Empty)
                    return false;

                auth.User = prefs.UserName;

                if (!prefs.Savepassword || prefs.Password != string.Empty)
                {
                    GetPass p = new GetPass();
                    p.Pass = prefs.Password;
                    if (p.ShowDialog(this) == DialogResult.OK)
                        auth.Pass = p.Pass;
                    else
                        return false;
                }
                else
                    auth.Pass = prefs.Password;
            }

            if (auth.User == string.Empty || auth.Pass == string.Empty)
                return false;

            // send login;
            DialogResult result = auth.ShowDialog(this);
            if (result != DialogResult.OK)
                return false;

            ID = auth.ID;
            Token = auth.Token;

            return true;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            if (Token == 0)
                Authenticate();

            if (Token == 0)
                return;

            Close();
        }

        protected void StartListServerLookup()
        {
            if (ListInProgress)
                return;

            ServerList.Items.Clear();

            if (Email == string.Empty)
                return;

            if (Token == 0)
                Authenticate();
            if (Token == 0)
                return;

            ListInProgress = true;
            Uri url = new Uri(RootHost.ListURL + "action=list&id=" + ID + "&tok=" + Token);
            ListClient.DownloadStringAsync(url);
        }

        void ListClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string[] servers = e.Result.Split("\n".ToCharArray());
            if (servers[0] != "OK")
            {
                // an error
            }
            else
            {
                if (servers.Length > 1)
                {
                    for (int i = 1; i < servers.Length; i++)
                        ServerList.Items.Add(new ServerListItem(servers[i]));
                }
                else
                    ServerList.Items.Add(new ServerListItem("127.0.0.1;Localhost;OPEN"));

                ServerList.SelectedIndex = 0;
            }

            ListInProgress = false;
        }

        private void Splash_Shown(object sender, EventArgs e)
        {
            if (!Prefs.Exists())
                registerToolStripMenuItem_Click(this, EventArgs.Empty);

            Prefs prefs = Prefs.Load();

            if (prefs.UserName == string.Empty)
                userSettingsToolStripMenuItem_Click(this, EventArgs.Empty);
            else
            {
                Email = prefs.UserName;
                if (prefs.Savepassword)
                    Password = prefs.Password;
            }

            if (ServerList.Items.Count == 0)
                StartListServerLookup();
        }
    }
}
