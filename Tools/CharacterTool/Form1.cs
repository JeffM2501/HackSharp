using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace CharacterTool
{
    public partial class Form1 : Form
    {
        protected List<Image> Faces = new List<Image>();

        protected List<Image> BoyHair = new List<Image>();
        protected List<Image> GirlHair = new List<Image>();
        protected List<Image> Hairs = new List<Image>();

        protected List<Image> BoyOutfits = new List<Image>();
        protected List<Image> GirlOutfits = new List<Image>();
        protected List<Image> Outfits = new List<Image>();

        protected List<Image> Boys = new List<Image>();
        protected List<Image> Girls = new List<Image>();

        protected List<string> BoyNames = new List<string>();
        protected List<string> GirlNames = new List<string>();

        protected Timer timer = null;
        protected Timer timer2 = null;

        protected int Dir = 0;
        protected int Frame = 0;
        protected int FrameDir = 1;

        protected Dictionary<Image, int> ImageListIndexLookup = new Dictionary<Image, int>();
       
        List<string> BoyHairNames = new List<string>();
        List<string> GirlHairNames = new List<string>();
        List<string> HairNames = new List<string>();

        List<string> OutfitNames = new List<string>();
        List<string> BoyOutfitNames = new List<string>();
        List<string> GirlOutfitNames = new List<string>();

        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 500;
            timer2 = new Timer();
            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = 150;

            DirectoryInfo dir = new DirectoryInfo("data");
            if (!dir.Exists)
            {
                dir = new DirectoryInfo("../data"); 
                if (!dir.Exists)
                {
                    dir = new DirectoryInfo("../../data"); 
                    if (!dir.Exists)
                    {
                        dir = new DirectoryInfo("../../../data");
                        if (!dir.Exists)
                            return;
                    }
                }
            }

            List<string> faceNames = new List<string>();

            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, "Hair")), ref Hairs, ref HairNames);
            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Hair", "Male"))), ref BoyHair, ref BoyHairNames);
            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Hair", "Female"))), ref GirlHair, ref GirlHairNames);

            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, "Outfit")), ref Outfits, ref OutfitNames);
            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Outfit", "Male"))), ref BoyOutfits, ref BoyOutfitNames);
            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Outfit", "Female"))), ref GirlOutfits, ref GirlOutfitNames);
      
            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Face", "Human"))), ref Faces, ref faceNames);

            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Body","Male"))), ref Boys, ref BoyNames);
            LoadImagesFromDir(new DirectoryInfo(Path.Combine(dir.FullName, Path.Combine("Body", "Female"))), ref Girls, ref GirlNames);

            BodyList.ImageList = new ImageList();
            BodyList.ImageList.ColorDepth = ColorDepth.Depth24Bit;
            BodyList.ImageList.ImageSize = new System.Drawing.Size(16, 8);

            int alpha = 255;
            BodyList.ImageList.Images.Add(GetListImageFromColor(Color.FromArgb(alpha, 255, 231, 156)));
            BodyList.ImageList.Images.Add(GetListImageFromColor(Color.FromArgb(alpha, 248, 188, 152)));
            BodyList.ImageList.Images.Add(GetListImageFromColor(Color.FromArgb(alpha, 246, 189, 123)));
            BodyList.ImageList.Images.Add(GetListImageFromColor(Color.FromArgb(alpha, 208, 168, 128)));
            BodyList.ImageList.Images.Add(GetListImageFromColor(Color.FromArgb(alpha, 149, 98, 36)));
            BodyList.ImageList.Images.Add(GetListImageFromColor(Color.FromArgb(alpha, 96, 39, 0)));

            SetBodyNames(GirlNames);

            Point p = new Point(1, 2);
            foreach (Bitmap img in Hairs)
            {
                HairImageList.Images.Add(ClipImageSection(img, p));
                ImageListIndexLookup.Add(img, HairImageList.Images.Count-1);
            }

            foreach (Bitmap img in BoyHair)
            {
                HairImageList.Images.Add(ClipImageSection(img, p));
                ImageListIndexLookup.Add(img, HairImageList.Images.Count - 1);
            }

            foreach (Bitmap img in GirlHair)
            {
                HairImageList.Images.Add(ClipImageSection(img, p));
                ImageListIndexLookup.Add(img, HairImageList.Images.Count - 1);
            }

            foreach (Bitmap img in Outfits)
            {
                OutfitImageList.Images.Add(ClipImageSection(img, p));
                ImageListIndexLookup.Add(img, OutfitImageList.Images.Count - 1);
            }

            foreach (Bitmap img in BoyOutfits)
            {
                OutfitImageList.Images.Add(ClipImageSection(img, p));
                ImageListIndexLookup.Add(img, OutfitImageList.Images.Count - 1);
            }

            foreach (Bitmap img in GirlOutfits)
            {
                OutfitImageList.Images.Add(ClipImageSection(img, p));
                ImageListIndexLookup.Add(img, OutfitImageList.Images.Count - 1);
            }

            foreach (Bitmap img in Faces)
                FaceImageList.Images.Add(ClipImageSection(img, p));

            for (int i = 0; i < faceNames.Count; i++)
                FaceListView.Items.Add(new ListViewItem(faceNames[i], i));

            BuildGenderLists();

            BodyList.SelectedIndex = 0;
            FaceListView.Items[0].Selected = true;
            HairListView.Items[0].Selected = true;
            OutfitListView.Items[new Random().Next(OutfitListView.Items.Count)].Selected = true;
            
            MaleRadio.Checked = false;
            FemaleRadio.Checked = true;

            Dir = 2;
            Animate.Checked = true;
            timer.Start();
            timer2.Start();
        }

        void BuildGenderLists ()
        {
            // hairs
            HairListView.Items.Clear();
            for (int i = 0; i < HairNames.Count; i++)
            {
                ListViewItem item = new ListViewItem(HairNames[i], ImageListIndexLookup[Hairs[i]]);
                item.Group = HairListView.Groups[0];
                item.Tag = Hairs[i];
                HairListView.Items.Add(item);
            }

            if (MaleRadio.Checked)
            {
                for (int i = 0; i < BoyHairNames.Count; i++)
                {
                    ListViewItem item = new ListViewItem(BoyHairNames[i], ImageListIndexLookup[BoyHair[i]]);
                    item.Group = HairListView.Groups[1];
                    item.Tag = BoyHair[i];
                    HairListView.Items.Add(item);
                }
            }
            else if (FemaleRadio.Checked)
            {
                for (int i = 0; i < GirlHairNames.Count; i++)
                {
                    ListViewItem item = new ListViewItem(BoyHairNames[i], ImageListIndexLookup[GirlHair[i]]);
                    item.Group = HairListView.Groups[2];
                    item.Tag = GirlHair[i];
                    HairListView.Items.Add(item);
                }
            }

            // outfits
            OutfitListView.Items.Clear();
            for (int i = 0; i < OutfitNames.Count; i++)
            {
                ListViewItem item = new ListViewItem(OutfitNames[i], ImageListIndexLookup[Outfits[i]]);
                item.Group = OutfitListView.Groups[0];
                item.Tag = Outfits[i];
                OutfitListView.Items.Add(item);
            }

            if (MaleRadio.Checked)
            {
                for (int i = 0; i < BoyOutfitNames.Count; i++)
                {
                    ListViewItem item = new ListViewItem(BoyOutfitNames[i], ImageListIndexLookup[BoyOutfits[i]]);
                    item.Group = OutfitListView.Groups[1];
                    item.Tag = BoyOutfits[i];
                    OutfitListView.Items.Add(item);
                }
            }
            else if (FemaleRadio.Checked)
            {
                for (int i = 0; i < GirlOutfitNames.Count; i++)
                {
                    ListViewItem item = new ListViewItem(GirlOutfitNames[i], ImageListIndexLookup[GirlOutfits[i]]);
                    item.Group = OutfitListView.Groups[2];
                    item.Tag = GirlOutfits[i];
                    OutfitListView.Items.Add(item);
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (Spin.Checked)
            {
                Dir++;
                if (Dir > 3)
                    Dir = 0;
            }
            pictureBox1.Invalidate();
        }


        void timer2_Tick(object sender, EventArgs e)
        {
            if (Animate.Checked)
            {
                Frame += FrameDir;
                if (Frame > 2)
                {
                    Frame = 1;
                    FrameDir = -1;
                }
                else if (Frame < 0)
                {
                    Frame = 1;
                    FrameDir = 1;
                }
            }

            pictureBox1.Invalidate();
        }

        protected void SetBodyNames ( List<string> names )
        {
            BodyList.Items.Clear();
            foreach (string name in names)
                BodyList.Items.Add(new ComboBoxExItem(name, BodyList.Items.Count));
        }

        protected void LoadImagesFromDir(DirectoryInfo dir, ref List<Image> list, ref List<string> names )
        {
            if (!dir.Exists)
                return;

            foreach (FileInfo file in dir.GetFiles("*.png"))
            {
                try
                {
                    Image img = new Bitmap(file.FullName);
                    list.Add(img);
                    names.Add(Path.GetFileNameWithoutExtension(file.Name));
                }
                catch (System.Exception ex)
                {
                }
            }
        }

        protected Image GetListImageFromColor ( Color color )
        {
            Bitmap map = new Bitmap(BodyList.ImageList.ImageSize.Width, BodyList.ImageList.ImageSize.Height);
            Graphics graph = Graphics.FromImage(map);
            graph.Clear(color);
            graph.Dispose();
            return map;
        }

        protected Image GetBodyImage ()
        {
            if (FemaleRadio.Checked)
                return Girls[BodyList.SelectedIndex];

            return Boys[BodyList.SelectedIndex];
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.CornflowerBlue);
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.TranslateTransform(25, 25);
            e.Graphics.ScaleTransform(4,4);

            Point Grid = new Point(Frame, Dir);

            if (BodyList.SelectedIndex >= 0)
                DrawImageSection(GetBodyImage(), e.Graphics, Grid);

            if (OutfitListView.SelectedIndices.Count > 0)
                DrawImageSection(OutfitListView.SelectedItems[0].Tag as Image, e.Graphics, Grid);

            if (FaceListView.SelectedIndices.Count > 0)
                DrawImageSection(Faces[FaceListView.SelectedIndices[0]], e.Graphics, Grid);

            if (HairListView.SelectedIndices.Count > 0)
                DrawImageSection(HairListView.SelectedItems[0].Tag as Image, e.Graphics, Grid);
        }

        private void Character_SelectionChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void FemaleRadio_CheckedChanged(object sender, EventArgs e)
        {
            BuildGenderLists();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        protected void DrawImageSection ( Image img, Graphics graphics, Point pos )
        {
            if (img == null)
                return;

            int SectionX = img.Width / 3;
            int SectionY = img.Height / 4;

            Rectangle srcRect = new Rectangle(pos.X * SectionX,pos.Y * SectionY,SectionX,SectionY);
            Rectangle destRect = new Rectangle(0,0,SectionX,SectionY);
            graphics.DrawImage(img,destRect,srcRect,GraphicsUnit.Pixel);
        }

        protected Bitmap ClipImageSection ( Bitmap img, Point pos )
        {
            int SectionX = img.Width / 3;
            int SectionY = img.Height / 4;
            Rectangle srcRect = new Rectangle(pos.X * SectionX, pos.Y * SectionY, SectionX, SectionY);
            Rectangle destRect = new Rectangle(0,0,SectionX,SectionY);

            Bitmap map = new Bitmap(SectionX,SectionY);
            Graphics destGraph = Graphics.FromImage(map);
            destGraph.Clear(Color.Transparent);
            destGraph.DrawImage(img,destRect,srcRect,GraphicsUnit.Pixel);
            destGraph.Dispose();
            return map;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
