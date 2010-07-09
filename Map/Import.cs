using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

using System.Xml;
using System.Xml.Serialization;

namespace Maps
{
    public class Import
    {
        protected static Color GetTileColor(int x, int y, Bitmap bitmap)
        {
            if (x >= bitmap.Width || x < 0)
                return Color.Black;
            
            if (y >= bitmap.Height || y < 0)
                return Color.Black;

            return bitmap.GetPixel(x, bitmap.Height - y -1);
        }

        protected static bool PosIsTile( int x, int y, Bitmap bitmap)
        {
            Color color = GetTileColor(x, y,bitmap);
            return color.R != 0 || color.G != 0 || color.B != 0;
        }

        protected static Direction[] FindWallDirections ( Point pos, Bitmap bitmap )
        {
            List<Direction> dirs = new List<Direction>();

            if (!PosIsTile(pos.X+1,pos.Y,bitmap))
                dirs.Add(Direction.East);

            if (!PosIsTile(pos.X - 1, pos.Y, bitmap))
                dirs.Add(Direction.West);
           
            if (!PosIsTile(pos.X, pos.Y - 1, bitmap))
                dirs.Add(Direction.South);

            if (!PosIsTile(pos.X, pos.Y + 1, bitmap))
                dirs.Add(Direction.North);

            return dirs.ToArray();
        }

        protected static bool IsDifferentRoom ( int x, int y, Bitmap bitmap, Color thisColor )
        {
            Color c = GetTileColor(x,y,bitmap);
            if ( c.R == 0 && c.G == 0 && c.B == 0)
                return false;

            return c != thisColor;
        }
        protected static bool IsDoor (Point pos, Bitmap bitmap)
        {
            if (!PosIsTile(pos.X, pos.Y, bitmap))
                return false;

            Color thisColor = GetTileColor(pos.X, pos.Y, bitmap);
            if (IsDifferentRoom(pos.X + 1, pos.Y, bitmap,thisColor))
                return true;
            if (IsDifferentRoom(pos.X - 1, pos.Y, bitmap,thisColor))
                return true;
            if (IsDifferentRoom(pos.X, pos.Y + 1, bitmap,thisColor))
                return true;
            if (IsDifferentRoom(pos.X, pos.Y - 1, bitmap,thisColor))
                return true;

            return false;
        }

        public static Map FromImage ( Image image )
        {
            Bitmap bitmap = new Bitmap(image);
            Map map = new Map();

            Dictionary<Color, Room> RoomIDMap = new Dictionary<Color, Room>();

            Surface wall = new Surface();
            wall.Texture = "default_wall";

            int wallID = 0;
            map.Surfaces.Add(wallID,wall);

            Surface floor = new Surface();
            floor.Texture = "default_floor";

            int floorID = 1;
            map.Surfaces.Add(floorID,floor);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    if (PosIsTile(x, y, bitmap))
                    {
                        Color c = bitmap.GetPixel(x, bitmap.Height - y- 1);

                        Room room = null;

                        if (RoomIDMap.ContainsKey(c))
                            room = RoomIDMap[c];
                        else
                        {
                            room = new Room();
                            RoomIDMap.Add(c, room);
                        }

                        Tile tile = new Tile();
                        tile.Location = new Point(x,y);
                        if (IsDoor(tile.Location, bitmap))
                            tile.Type = Tile.TileType.ClosedDoor;
                        else
                            tile.Type = Tile.TileType.Open;

                        tile.Floor = floorID;
                        foreach (Direction dir in FindWallDirections(tile.Location, bitmap))
                            tile.Walls.Add(dir, wallID);
                        room.Tiles.Add(tile);
                    }
                }
            }

            bitmap.Dispose();

            foreach (KeyValuePair<Color, Room> i in RoomIDMap)
                map.Rooms.Add(i.Value);
            return map;
        }

        public static Map FromXML ( FileInfo file )
        {
            XmlSerializer xml = new XmlSerializer(typeof(Map));

            if (!file.Exists)
                return null;

            Stream stream = file.OpenRead();

            Map map = (Map)xml.Deserialize(stream);
            stream.Close();

            return map;
        }
    }
}
