namespace PewClient
{
    partial class Splash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.updatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.ServerList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updatesToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(652, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // updatesToolStripMenuItem
            // 
            this.updatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem});
            this.updatesToolStripMenuItem.Name = "updatesToolStripMenuItem";
            this.updatesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.updatesToolStripMenuItem.Text = "Updates";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check For Updates";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoSettingsToolStripMenuItem,
            this.audioSettingsToolStripMenuItem,
            this.userSettingsToolStripMenuItem,
            this.registerToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // videoSettingsToolStripMenuItem
            // 
            this.videoSettingsToolStripMenuItem.Name = "videoSettingsToolStripMenuItem";
            this.videoSettingsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.videoSettingsToolStripMenuItem.Text = "Video Settings";
            // 
            // audioSettingsToolStripMenuItem
            // 
            this.audioSettingsToolStripMenuItem.Name = "audioSettingsToolStripMenuItem";
            this.audioSettingsToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.audioSettingsToolStripMenuItem.Text = "Audio Settings";
            // 
            // userSettingsToolStripMenuItem
            // 
            this.userSettingsToolStripMenuItem.Name = "userSettingsToolStripMenuItem";
            this.userSettingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.userSettingsToolStripMenuItem.Text = "User Settings";
            this.userSettingsToolStripMenuItem.Click += new System.EventHandler(this.userSettingsToolStripMenuItem_Click);
            // 
            // registerToolStripMenuItem
            // 
            this.registerToolStripMenuItem.Name = "registerToolStripMenuItem";
            this.registerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.registerToolStripMenuItem.Text = "Register";
            this.registerToolStripMenuItem.Click += new System.EventHandler(this.registerToolStripMenuItem_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 38);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(652, 301);
            this.webBrowser1.TabIndex = 2;
            this.webBrowser1.Url = new System.Uri("http://www.google.com", System.UriKind.Absolute);
            // 
            // ServerList
            // 
            this.ServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ServerList.FormattingEnabled = true;
            this.ServerList.Location = new System.Drawing.Point(12, 378);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(260, 21);
            this.ServerList.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Servers";
            // 
            // Play
            // 
            this.Play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Play.Location = new System.Drawing.Point(565, 376);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(75, 23);
            this.Play.TabIndex = 5;
            this.Play.Text = "Play";
            this.Play.UseVisualStyleBackColor = true;
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(325, 386);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(17, 13);
            this.VersionLabel.TabIndex = 6;
            this.VersionLabel.Text = "V.";
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 411);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Splash";
            this.Text = "Pew... A decidedly simple shooter";
            this.Load += new System.EventHandler(this.Splash_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem audioSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registerToolStripMenuItem;
        private System.Windows.Forms.ComboBox ServerList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label VersionLabel;
    }
}

