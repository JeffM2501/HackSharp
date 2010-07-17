namespace CharacterTool
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Universal", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Male", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Female", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Universal", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Male", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Female", System.Windows.Forms.HorizontalAlignment.Left);
            this.MaleRadio = new System.Windows.Forms.RadioButton();
            this.FemaleRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OutfitListView = new System.Windows.Forms.ListView();
            this.OutfitImageList = new System.Windows.Forms.ImageList(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HairListView = new System.Windows.Forms.ListView();
            this.HairImageList = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.FaceListView = new System.Windows.Forms.ListView();
            this.FaceImageList = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Animate = new System.Windows.Forms.CheckBox();
            this.Spin = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BodyList = new CharacterTool.ComboBoxEx();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MaleRadio
            // 
            this.MaleRadio.AutoSize = true;
            this.MaleRadio.Location = new System.Drawing.Point(16, 46);
            this.MaleRadio.Name = "MaleRadio";
            this.MaleRadio.Size = new System.Drawing.Size(48, 17);
            this.MaleRadio.TabIndex = 2;
            this.MaleRadio.TabStop = true;
            this.MaleRadio.Text = "Male";
            this.MaleRadio.UseVisualStyleBackColor = true;
            this.MaleRadio.CheckedChanged += new System.EventHandler(this.FemaleRadio_CheckedChanged);
            // 
            // FemaleRadio
            // 
            this.FemaleRadio.AutoSize = true;
            this.FemaleRadio.Location = new System.Drawing.Point(70, 46);
            this.FemaleRadio.Name = "FemaleRadio";
            this.FemaleRadio.Size = new System.Drawing.Size(59, 17);
            this.FemaleRadio.TabIndex = 3;
            this.FemaleRadio.TabStop = true;
            this.FemaleRadio.Text = "Female";
            this.FemaleRadio.UseVisualStyleBackColor = true;
            this.FemaleRadio.CheckedChanged += new System.EventHandler(this.FemaleRadio_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.OutfitListView);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.BodyList);
            this.groupBox1.Controls.Add(this.FemaleRadio);
            this.groupBox1.Controls.Add(this.MaleRadio);
            this.groupBox1.Location = new System.Drawing.Point(293, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 375);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Body";
            // 
            // OutfitListView
            // 
            this.OutfitListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            listViewGroup1.Header = "Universal";
            listViewGroup1.Name = "UnivversalOutfit";
            listViewGroup2.Header = "Male";
            listViewGroup2.Name = "MaleOutfit";
            listViewGroup3.Header = "Female";
            listViewGroup3.Name = "FemaleOutfit";
            this.OutfitListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2,
            listViewGroup3});
            this.OutfitListView.LargeImageList = this.OutfitImageList;
            this.OutfitListView.Location = new System.Drawing.Point(6, 82);
            this.OutfitListView.Name = "OutfitListView";
            this.OutfitListView.Size = new System.Drawing.Size(187, 285);
            this.OutfitListView.SmallImageList = this.OutfitImageList;
            this.OutfitListView.StateImageList = this.OutfitImageList;
            this.OutfitListView.TabIndex = 12;
            this.OutfitListView.TileSize = new System.Drawing.Size(150, 48);
            this.OutfitListView.UseCompatibleStateImageBehavior = false;
            this.OutfitListView.View = System.Windows.Forms.View.Tile;
            this.OutfitListView.SelectedIndexChanged += new System.EventHandler(this.Character_SelectionChanged);
            // 
            // OutfitImageList
            // 
            this.OutfitImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.OutfitImageList.ImageSize = new System.Drawing.Size(48, 48);
            this.OutfitImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Outfit";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Location = new System.Drawing.Point(498, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 415);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Head";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Hair";
            // 
            // HairListView
            // 
            this.HairListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            listViewGroup4.Header = "Universal";
            listViewGroup4.Name = "UniversalHair";
            listViewGroup5.Header = "Male";
            listViewGroup5.Name = "MaleHair";
            listViewGroup6.Header = "Female";
            listViewGroup6.Name = "FemaleHair";
            this.HairListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup4,
            listViewGroup5,
            listViewGroup6});
            this.HairListView.LargeImageList = this.HairImageList;
            this.HairListView.Location = new System.Drawing.Point(6, 19);
            this.HairListView.Name = "HairListView";
            this.HairListView.Size = new System.Drawing.Size(199, 171);
            this.HairListView.SmallImageList = this.HairImageList;
            this.HairListView.StateImageList = this.HairImageList;
            this.HairListView.TabIndex = 9;
            this.HairListView.TileSize = new System.Drawing.Size(150, 48);
            this.HairListView.UseCompatibleStateImageBehavior = false;
            this.HairListView.View = System.Windows.Forms.View.Tile;
            this.HairListView.SelectedIndexChanged += new System.EventHandler(this.Character_SelectionChanged);
            // 
            // HairImageList
            // 
            this.HairImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.HairImageList.ImageSize = new System.Drawing.Size(48, 48);
            this.HairImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Face";
            // 
            // FaceListView
            // 
            this.FaceListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FaceListView.LargeImageList = this.FaceImageList;
            this.FaceListView.Location = new System.Drawing.Point(0, 25);
            this.FaceListView.Name = "FaceListView";
            this.FaceListView.Size = new System.Drawing.Size(205, 160);
            this.FaceListView.SmallImageList = this.FaceImageList;
            this.FaceListView.StateImageList = this.FaceImageList;
            this.FaceListView.TabIndex = 11;
            this.FaceListView.TileSize = new System.Drawing.Size(150, 48);
            this.FaceListView.UseCompatibleStateImageBehavior = false;
            this.FaceListView.View = System.Windows.Forms.View.Tile;
            this.FaceListView.SelectedIndexChanged += new System.EventHandler(this.Character_SelectionChanged);
            // 
            // FaceImageList
            // 
            this.FaceImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.FaceImageList.ImageSize = new System.Drawing.Size(48, 48);
            this.FaceImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(4, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(283, 403);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Resize += new System.EventHandler(this.pictureBox1_Resize);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // Animate
            // 
            this.Animate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Animate.AutoSize = true;
            this.Animate.Location = new System.Drawing.Point(14, 3);
            this.Animate.Name = "Animate";
            this.Animate.Size = new System.Drawing.Size(64, 17);
            this.Animate.TabIndex = 7;
            this.Animate.Text = "Animate";
            this.Animate.UseVisualStyleBackColor = true;
            // 
            // Spin
            // 
            this.Spin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Spin.AutoSize = true;
            this.Spin.Location = new System.Drawing.Point(84, 3);
            this.Spin.Name = "Spin";
            this.Spin.Size = new System.Drawing.Size(47, 17);
            this.Spin.TabIndex = 8;
            this.Spin.Text = "Spin";
            this.Spin.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.Animate);
            this.panel1.Controls.Add(this.Spin);
            this.panel1.Location = new System.Drawing.Point(293, 377);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(136, 28);
            this.panel1.TabIndex = 9;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 19);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.HairListView);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.FaceListView);
            this.splitContainer1.Size = new System.Drawing.Size(208, 383);
            this.splitContainer1.SplitterDistance = 191;
            this.splitContainer1.TabIndex = 10;
            // 
            // BodyList
            // 
            this.BodyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BodyList.FormattingEnabled = true;
            this.BodyList.ImageList = null;
            this.BodyList.Location = new System.Drawing.Point(16, 19);
            this.BodyList.Name = "BodyList";
            this.BodyList.Size = new System.Drawing.Size(177, 21);
            this.BodyList.TabIndex = 0;
            this.BodyList.SelectedIndexChanged += new System.EventHandler(this.Character_SelectionChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(719, 417);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBoxEx BodyList;
        private System.Windows.Forms.RadioButton MaleRadio;
        private System.Windows.Forms.RadioButton FemaleRadio;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox Animate;
        private System.Windows.Forms.CheckBox Spin;
        private System.Windows.Forms.ListView HairListView;
        private System.Windows.Forms.ImageList HairImageList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView FaceListView;
        private System.Windows.Forms.ImageList FaceImageList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView OutfitListView;
        private System.Windows.Forms.ImageList OutfitImageList;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

