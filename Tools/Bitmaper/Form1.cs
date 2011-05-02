using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

using System.Windows.Forms;

namespace Bitmaper
{
    public partial class Form1 : Form
    {
        IsoMap Map = null;

        string MapPath = string.Empty;

        Point MapGraphicsOrigin = new Point(0, 0);
        Point LastMousePos = Point.Empty;

        bool ShowCollisions = false;
        bool ShowObjects = true;

        public Form1()
        {
            InitializeComponent();

            IsoMap.LocateFile = new IsoMap.UnknownFileHandler(FindFile);
            oToolStripMenuItem.Checked = ShowObjects;
            invisiblesToolStripMenuItem.Checked = ShowCollisions;
        }

        protected string FindFile(string input)
        {
            string p = Path.Combine(MapPath, input);
            if (!File.Exists(p))
                return input;

            return new FileInfo(p).FullName;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                MapPath = Path.GetDirectoryName(ofd.FileName);

                Map = ManaWorldMapReader.FromXML(ofd.FileName);
                FillTilesetList();
                this.Text = Path.GetFileNameWithoutExtension(ofd.FileName);

                MainView.Invalidate();
            }
        }

        private void MainView_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.CornflowerBlue);
            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.ScaleTransform(0.5f, 0.5f);

            if (Map != null)
                Map.Draw(ShowCollisions, ShowObjects, MapGraphicsOrigin, e.Graphics,MainView.Bounds);
        }

        private void MainView_MouseDown(object sender, MouseEventArgs e)
        {
            MainView.Capture = true;
            LastMousePos = new Point(e.X, e.Y);
        }

        private void MainView_MouseUp(object sender, MouseEventArgs e)
        {
            MainView.Capture = false;
        }

        private void MainView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MapGraphicsOrigin.X += e.X - LastMousePos.X;
                MapGraphicsOrigin.Y += e.Y - LastMousePos.Y;
                MainView.Invalidate();
            }
            LastMousePos = new Point(e.X, e.Y);
        }

        protected void FillTilesetList()
        {
            TilesetList.Items.Clear();
            foreach (KeyValuePair<int,MapImage> img in Map.Tilesets)
                TilesetList.Items.Add(img.Value);

            TilesetList.SelectedIndex = 0;
        }

        private void TilesetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            TilesetItems.Items.Clear();

            MapImage img = TilesetList.SelectedItem as MapImage;
            if (img == null)
                return;

            ImageList bList = new ImageList();
            ImageList sList = new ImageList();

            bList.ImageSize = new Size(32, 32);
            sList.ImageSize = new Size(16, 16);

            for (int y = 0; y < img.Bounds.Height; y++)
            {
                for (int x = 0; x < img.Bounds.Width; x++)
                {
                    bList.Images.Add(img.GetGird(new Point(x, y)));
                    sList.Images.Add(img.GetGird(new Point(x, y)));
                    ListViewItem itm = new ListViewItem(x.ToString() + "," + y.ToString(), bList.Images.Count - 1);
                    TilesetItems.Items.Add(itm);
                }
            }

            TilesetItems.LargeImageList = bList;
            TilesetItems.SmallImageList = sList;
        }

        private void invisiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCollisions = !ShowCollisions;
            invisiblesToolStripMenuItem.Checked = ShowCollisions;
            MainView.Invalidate();
        }

        private void oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowObjects = !ShowObjects;
            oToolStripMenuItem.Checked = ShowObjects;
            MainView.Invalidate();
        }
    }
}
