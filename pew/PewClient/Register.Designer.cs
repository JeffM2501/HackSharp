namespace PewClient
{
    partial class Register
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
            this.OK = new System.Windows.Forms.Button();
            this.CANCEL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.TextBox();
            this.Callsign = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CheckName = new System.Windows.Forms.Button();
            this.Password1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Password2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Agree = new System.Windows.Forms.CheckBox();
            this.Terms = new System.Windows.Forms.LinkLabel();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.Location = new System.Drawing.Point(212, 206);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // CANCEL
            // 
            this.CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CANCEL.Location = new System.Drawing.Point(131, 206);
            this.CANCEL.Name = "CANCEL";
            this.CANCEL.Size = new System.Drawing.Size(75, 23);
            this.CANCEL.TabIndex = 1;
            this.CANCEL.Text = "Cancel";
            this.CANCEL.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "E-Mail";
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(5, 29);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(267, 20);
            this.Email.TabIndex = 3;
            this.Email.TextChanged += new System.EventHandler(this.Email_TextChanged);
            // 
            // Callsign
            // 
            this.Callsign.Location = new System.Drawing.Point(5, 148);
            this.Callsign.Name = "Callsign";
            this.Callsign.Size = new System.Drawing.Size(267, 20);
            this.Callsign.TabIndex = 5;
            this.Callsign.TextChanged += new System.EventHandler(this.Callsign_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Callsign";
            // 
            // CheckName
            // 
            this.CheckName.Location = new System.Drawing.Point(212, 173);
            this.CheckName.Name = "CheckName";
            this.CheckName.Size = new System.Drawing.Size(75, 23);
            this.CheckName.TabIndex = 6;
            this.CheckName.Text = "Check";
            this.CheckName.UseVisualStyleBackColor = true;
            this.CheckName.Click += new System.EventHandler(this.CheckName_Click);
            // 
            // Password1
            // 
            this.Password1.Location = new System.Drawing.Point(5, 68);
            this.Password1.Name = "Password1";
            this.Password1.PasswordChar = '*';
            this.Password1.Size = new System.Drawing.Size(267, 20);
            this.Password1.TabIndex = 8;
            this.Password1.TextChanged += new System.EventHandler(this.Password1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // Password2
            // 
            this.Password2.Location = new System.Drawing.Point(5, 107);
            this.Password2.Name = "Password2";
            this.Password2.PasswordChar = '*';
            this.Password2.Size = new System.Drawing.Size(267, 20);
            this.Password2.TabIndex = 10;
            this.Password2.TextChanged += new System.EventHandler(this.Password2_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Password (Again)";
            // 
            // Agree
            // 
            this.Agree.AutoSize = true;
            this.Agree.Location = new System.Drawing.Point(5, 179);
            this.Agree.Name = "Agree";
            this.Agree.Size = new System.Drawing.Size(89, 17);
            this.Agree.TabIndex = 11;
            this.Agree.Text = "I agree to the";
            this.Agree.UseVisualStyleBackColor = true;
            // 
            // Terms
            // 
            this.Terms.AutoSize = true;
            this.Terms.Location = new System.Drawing.Point(87, 180);
            this.Terms.Name = "Terms";
            this.Terms.Size = new System.Drawing.Size(109, 13);
            this.Terms.TabIndex = 12;
            this.Terms.TabStop = true;
            this.Terms.Text = "Terms and Conditions";
            this.Terms.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Terms_LinkClicked);
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.ErrorLabel.Location = new System.Drawing.Point(5, 216);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(46, 13);
            this.ErrorLabel.TabIndex = 13;
            this.ErrorLabel.Text = "ERROR";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 241);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.Terms);
            this.Controls.Add(this.Agree);
            this.Controls.Add(this.Password2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Password1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CheckName);
            this.Controls.Add(this.Callsign);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CANCEL);
            this.Controls.Add(this.OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Register";
            this.Text = "Registration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button CANCEL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox Callsign;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CheckName;
        private System.Windows.Forms.TextBox Password1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Password2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox Agree;
        private System.Windows.Forms.LinkLabel Terms;
        private System.Windows.Forms.Label ErrorLabel;
    }
}