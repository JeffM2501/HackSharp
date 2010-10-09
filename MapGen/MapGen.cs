using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Maps;

namespace MapGen
{
    public partial class MapGenForm : Form
    {
        public Map map = new Map();
        Random rand = new Random();

        protected int GridSize = 16;

        Point LastMousePos = Point.Empty;

        Point ViewOffset = new Point(0, 0);

        public Dictionary<Room, Color> RoomColorMap = new Dictionary<Room, Color>();

        public MapGenForm()
        {
            InitializeComponent();
        }

        private void heroQuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateHeroquestMap();
        }

        private void MapViewBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Wheat);
            e.Graphics.TranslateTransform(0, MapViewBox.Height);
            e.Graphics.TranslateTransform(ViewOffset.X, ViewOffset.Y);
            e.Graphics.ScaleTransform(1, -1);

            foreach ( Room room in map.Rooms)
            {
                DrawRoom(e.Graphics, room);
            }
        }

        private void DrawRoom ( Graphics graphics, Room room )
        {
            if (!RoomColorMap.ContainsKey(room))
                RoomColorMap.Add(room,Color.FromArgb(255, rand.Next(255), rand.Next(255), rand.Next(255)));

            Color color = RoomColorMap[room];

            Brush brush = new SolidBrush(color);

            foreach (Tile tile in room.Tiles)
                DrawTile(graphics, brush, tile);

            brush.Dispose();
        }

        private void DrawTile ( Graphics graphics, Brush brush, Tile tile )
        {
            graphics.FillRectangle(brush, tile.Location.X * GridSize, tile.Location.Y * GridSize, GridSize, GridSize);
            graphics.DrawRectangle(Pens.White, tile.Location.X * GridSize, tile.Location.Y * GridSize, GridSize, GridSize);

            if (tile.Walls.ContainsKey(Direction.North))
                graphics.DrawLine(Pens.Black, tile.Location.X * GridSize, tile.Location.Y * GridSize + GridSize, tile.Location.X * GridSize + GridSize, tile.Location.Y * GridSize + GridSize);
            if (tile.Walls.ContainsKey(Direction.South))
                graphics.DrawLine(Pens.Black, tile.Location.X * GridSize, tile.Location.Y * GridSize, tile.Location.X * GridSize + GridSize, tile.Location.Y * GridSize);
            
            if (tile.Walls.ContainsKey(Direction.West))
                graphics.DrawLine(Pens.Black, tile.Location.X * GridSize, tile.Location.Y * GridSize, tile.Location.X * GridSize, tile.Location.Y * GridSize + GridSize);
            if (tile.Walls.ContainsKey(Direction.East))
                graphics.DrawLine(Pens.Black, tile.Location.X * GridSize + GridSize, tile.Location.Y * GridSize, tile.Location.X * GridSize + GridSize, tile.Location.Y * GridSize + GridSize);

            Pen p = Pens.Transparent;

            switch (tile.Type)
            {
                case Tile.TileType.ClosedDoor:
                    p = Pens.Brown;
                    break;

                case Tile.TileType.OpenDoor:
                    p = Pens.BurlyWood;
                    break;

                case Tile.TileType.Blocked:
                    p = Pens.Red;
                    break;

                case Tile.TileType.Unknown:
                    p = Pens.WhiteSmoke;
                    break;
            }

            if (p != Pens.Transparent)
                graphics.DrawRectangle(p, tile.Location.X * GridSize + GridSize * 0.25f, tile.Location.Y * GridSize + GridSize * 0.25f, GridSize * 0.5f, GridSize * 0.5f);
        }

        private void fromImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                map = Import.FromImage(Bitmap.FromFile(ofd.FileName));
                MapViewBox.Invalidate();
            }
        }

        protected void GenerateHeroquestMap ()
        {
            map.Clear();
        }

        private void MapGenForm_Resize(object sender, EventArgs e)
        {
            MapViewBox.Invalidate();
        }

        private void MapViewBox_MouseDown(object sender, MouseEventArgs e)
        {
            LastMousePos = new Point(e.X, e.Y);
        }

        private void MapViewBox_MouseUp(object sender, MouseEventArgs e)
        {
            LastMousePos = new Point(e.X, e.Y);
        }

        private void MapViewBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point delta = new Point(e.Location.X - LastMousePos.X, e.Location.Y - LastMousePos.Y);

            if (e.Button == MouseButtons.Left)
            {
                ViewOffset.X += delta.X;
                ViewOffset.Y += delta.Y;
                MapViewBox.Invalidate();
            }

            LastMousePos = new Point(e.X, e.Y);
        }
    }
}
