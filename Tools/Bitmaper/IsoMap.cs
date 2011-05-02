using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace Bitmaper
{
    public class MapImage
    {
        public int ID = 0;
        public string FileName = string.Empty;
        public string Name = string.Empty;
        public Image ImageData = null;

        protected Size BoundsCache = Size.Empty;
        public Size Bounds
        {
            get
            {
                if (BoundsCache == Size.Empty && FileName != null)
                {
                    Image img = GetImage();
                    BoundsCache = new Size(img.Width / Grid.Width, img.Height / Grid.Height);
                }

                return BoundsCache;
            }
        }
        public Size Grid = Size.Empty;

        public Object Tag = null;

        public override string ToString()
        {
            return Name;
        }

        public int Width
        {
            get { return Bounds.Width / Grid.Width; }
        }

        public int Height
        {
            get { return Bounds.Height / Grid.Height; }
        }

        protected Image GetImage ( )
        {
            if (ImageData != null )
                return ImageData;

            if (FileName == string.Empty)
                return null;

            string newFile = FileName;
            if (IsoMap.LocateFile != null)
                newFile = IsoMap.LocateFile(FileName);

            if (!File.Exists(newFile))
                return null;

            ImageData = Bitmap.FromFile(newFile);

            return ImageData;
        }

        public Image GetGird ( Point pos )
        {
            Bitmap map = new Bitmap(Grid.Width,Grid.Height);
            Graphics gfx = Graphics.FromImage(map);

            gfx.Clear(Color.Transparent);
            ImageDrawGrid(pos,Point.Empty,gfx);
            gfx.Dispose();

            return map;
        }

        public void ImageDrawGrid ( Point gridPos, Point pixelPos , Graphics gfx)
        {
            Rectangle srcRect = new Rectangle(gridPos.X * Grid.Width, gridPos.Y * Grid.Height, Grid.Width, Grid.Height);
            Rectangle destRect = new Rectangle(pixelPos.X, pixelPos.Y, Grid.Width, Grid.Height);
            gfx.DrawImage(GetImage(), destRect, srcRect, GraphicsUnit.Pixel);
        }
    }

    public class MapLayerCell
    {
        public MapImage Source = null;
        public Point Cell = Point.Empty;
    }

    public class MapLayer
    {
        public string Name = string.Empty;
        public List<MapLayerCell> Cells = new List<MapLayerCell>();
    }

    public class MapObject
    {
        public string Name = string.Empty;
        public string ObjectType = string.Empty;
        public Size Bounds = Size.Empty;
        public Point Origin = Point.Empty;
        public List<KeyValuePair<string, string>> Properties = new List<KeyValuePair<string, string>>();
    }

    public class IsoMap
    {
        public Dictionary<int, MapImage> Tilesets = new Dictionary<int, MapImage>();

        public List<MapLayer> BackgroundLayers = new List<MapLayer>();
        public List<MapLayer> ForeLayers = new List<MapLayer>();
      //  public List<MapLayer> InvisibleLayers = new List<MapLayer>();

        public List<MapObject> Objects = new List<MapObject>();

        public delegate string UnknownFileHandler(string input);
        public static UnknownFileHandler LocateFile = null;

        public class MapOptions
        {
            public bool Orthogonal = false;
            public Size Bounds = Size.Empty;
            public Size Grid = new Size(32, 32);
            public string Music = string.Empty;
            public string Name = string.Empty;
        }

        public MapOptions Options = new MapOptions();

        public List<bool> CollisionMap = new List<bool>();

        protected void DrawLayer(MapLayer layer, Point originGraphics, Graphics graphics, Rectangle visBounds)
        {
            for (int y = visBounds.Top; y < visBounds.Bottom; y++)
            {
                for (int x = visBounds.Left; x < visBounds.Right; x++)
                {
                    int index = x + y * Options.Bounds.Width;
                    if (layer.Cells[index].Source != null)
                        layer.Cells[index].Source.ImageDrawGrid(layer.Cells[index].Cell, new Point((x * Options.Grid.Width) + originGraphics.X, (y * Options.Grid.Height) - layer.Cells[index].Source.Grid.Height + Options.Grid.Height + originGraphics.Y), graphics);
                }
            }
        }

        protected void DrawCollisions(Point originGraphics, Graphics graphics, Rectangle visBounds)
        {
            for (int y = visBounds.Top; y < visBounds.Bottom; y++)
            {
                for (int x = visBounds.Left; x < visBounds.Right; x++)
                {
                    int index = x + y * Options.Bounds.Width;
                    if (CollisionMap[index])
                    {
                        Brush brush = new SolidBrush(Color.FromArgb(64, Color.Red));

                        Rectangle rect = new Rectangle(x * Options.Grid.Width + originGraphics.X + 2, y * Options.Grid.Height + originGraphics.Y + 2, Options.Grid.Width - 4, Options.Grid.Height - 4);
                        graphics.FillRectangle(brush, rect);

                        Pen pen = Pens.OrangeRed;
                        graphics.DrawRectangle(pen, rect);
                    }
                }
            }
        }

        protected void DrawObjects(MapObject obj, Point originGraphics, Graphics graphics)
        {
            Brush brush = new SolidBrush(Color.FromArgb(64, Color.White));
            Brush textBrush = new SolidBrush(Color.Black);

            Rectangle rect = new Rectangle(obj.Origin.X+originGraphics.X,obj.Origin.Y+originGraphics.Y,obj.Bounds.Width,obj.Bounds.Height);
            graphics.FillRectangle(brush, rect);

            Pen pen = Pens.Wheat;
            graphics.DrawRectangle(pen, rect);

            graphics.DrawString(obj.Name,new Font(FontFamily.GenericSansSerif,12),textBrush,new Point(obj.Origin.X+originGraphics.X,obj.Origin.Y+originGraphics.Y));
        }

        protected int SmallestPositive(int i)
        {
            if (i < 0)
                return 0;
            return i;
        }

        protected Rectangle ComputeVizGrid(Rectangle view, Point originGraphics)
        {
            int windowOriginXinGrid = SmallestPositive(-originGraphics.X / Options.Grid.Width);
            int windowOriginYinGrid = SmallestPositive(-originGraphics.Y / Options.Grid.Height);

            int windowBoundsXinGrid = SmallestPositive((-originGraphics.X + view.Width + view.Width) / Options.Grid.Width);
            int windowBoundsYinGrid = SmallestPositive((-originGraphics.Y + view.Height + view.Height) / Options.Grid.Height);

            if (windowBoundsXinGrid > Options.Bounds.Width)
                windowBoundsXinGrid = Options.Bounds.Width - 1;

            if (windowBoundsYinGrid > Options.Bounds.Height)
                windowBoundsYinGrid = Options.Bounds.Height - 1;

            return new Rectangle(windowOriginXinGrid, windowOriginYinGrid, windowBoundsXinGrid - windowOriginXinGrid, windowBoundsYinGrid - windowOriginYinGrid);
        }

        public void Draw(bool drawCollisions ,bool drawObjects, Point originGraphics, Graphics graphics, Rectangle bounds )
        {
            Rectangle visBounds = ComputeVizGrid(bounds, originGraphics);

            // draw the background layers
            foreach (MapLayer layer in BackgroundLayers)
                DrawLayer(layer, originGraphics, graphics, visBounds);
          
            foreach (MapLayer layer in ForeLayers)
                DrawLayer(layer,originGraphics,graphics, visBounds);

            if (drawCollisions)
                DrawCollisions(originGraphics, graphics, visBounds);

            if (drawObjects)
            {
                foreach (MapObject obj in Objects)
                    DrawObjects(obj, originGraphics, graphics);
            }
        }
    }
}
