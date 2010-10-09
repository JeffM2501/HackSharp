using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using Resources;

namespace Maps
{
    public enum Direction
    {
        North,
        South,
        East,
        West,
    }

    public class Surface
    {
        public static int Empty = -1;
        public string Texture = string.Empty;
        public Object Renderer = null;
    }

    public class ItemType
    {
        public string Name = string.Empty;
        public string Description = string.Empty;
        public string Texture = string.Empty;

        public static ItemType Empty = new ItemType();
        public Object Renderer = null;

        static int LastID = 0;
        public int ID = LastID++;
    }

    public class Item
    {
        [System.Xml.Serialization.XmlIgnoreAttribute]
        public ItemType Type = ItemType.Empty;

        public int ItemTypeID = -1;

        public Point Location = Point.Empty;
        public Object Tag = null;
    }

    public class TileAttribute
    {
    }

    public class Tile
    {
        public Point Location = Point.Empty;
        public Dictionary<Direction, int> Walls = new Dictionary<Direction, int>();
        public int Floor = Surface.Empty;
        public List<Item> Contents = new List<Item>();
        public Object Renderer = null;
        public Object Tag = null;

        public enum TileType
        {
            Open,
            Blocked,
            OpenDoor,
            ClosedDoor,
            Unknown,
        }

        public TileType Type = TileType.Unknown;

        public List<TileAttribute> Attributes = new List<TileAttribute>();
    }

    public class Room
    {
        public int ID = -1;
        public List<Tile> Tiles = new List<Tile>();
        public string Name = string.Empty;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public List<Door> Doors = new List<Door>();
        public List<int> DoorIDs = new List<int>();

        static int LastID = 0;
        public Room()
        {
            ID = LastID++;
        }
    }

    public class Actor
    {
        public Point Location = Point.Empty;
        public Tile MapLocation = null;
        public Object Renderer = null;
        public Object Tag = null;
    }

    public class AnimatedActor : Actor
    {
        public String TextureDir = string.Empty;
    }

    public class Monster : AnimatedActor
    {
        public String Name = string.Empty;
    }

    public class Adventurer : AnimatedActor
    {
        public String Name = string.Empty;
    }

    public class Door
    {
        public int ID = -1;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Room[] Destinations;

        public int[] DestinationIDs;
        public bool Open = false;
        public Point Location = Point.Empty;
        public int Rotation = 0;
        public bool Centered = false;

        static int LastID = 0;
        public Door()
        {
            ID = LastID++;
        }
    }

    public class Map
    {
        public Dictionary<int, Surface> Surfaces = new Dictionary<int, Surface>();

        public List<Room> Rooms = new List<Room>();
        public List<Door> Doors = new List<Door>();

        public List<ItemType> Objects = new List<ItemType>();
        public List<Actor> Actors = new List<Actor>();

        public void Clear ()
        {
            Surfaces.Clear();
            Rooms.Clear();
            Objects.Clear();
            Actors.Clear();
        }
    }
}
