using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Maps;

namespace MapGen
{
    class HeroQuestPart
    {
        public virtual bool Room
        {
            get { return true; }
        }

        public Size GridSize = Size.Empty;
        public Point Origin = Point.Empty;

        public bool Horizontal = false;

        public virtual Rectangle GetRect()
        {
            if (Horizontal)
                return new Rectangle(Origin.X,Origin.Y, GridSize.Height,GridSize.Height);

            return new Rectangle(Origin, GridSize);
        }

        public List<HeroQuestDoor> Doors = new List<HeroQuestDoor>();

        public List<HeroQuestPart> NorthLinks = new List<HeroQuestPart>();
        public List<HeroQuestPart> SouthLinks = new List<HeroQuestPart>();
        public List<HeroQuestPart> EastLinks = new List<HeroQuestPart>();
        public List<HeroQuestPart> WestLinks = new List<HeroQuestPart>();

        public Color MapColor = Color.Black;
    }

    class HeroQuestDoor : HeroQuestPart
    {
        public override bool Room
        {
            get { return false; }
        }

        public bool Secret = false;
        HeroQuestPart Host = null;
        HeroQuestPart Destination = null;
    }

    class HeroQuestRoom : HeroQuestPart
    {
        public override bool Room
        {
            get { return true; }
        }
    }

    class HeroQuestPassage : HeroQuestPart
    {
        public override bool Room
        {
            get { return false; }
        }

        public bool DeadEnd = false;
        public bool Stairs = false;

        public bool Start = false;
    }

    class HeroQuestConnector : HeroQuestPart
    {
        public override bool Room
        {
            get { return false; }
        }

        public bool Corner = false;

        public HeroQuestConnector()
        {
            GridSize = new Size(2, 2);
        }
    }
   
    class HeroQuestGenerator
    {
        protected List<HeroQuestConnector> Connectors = new List<HeroQuestConnector>();
        protected List<HeroQuestPassage> Passages = new List<HeroQuestPassage>();
        protected List<HeroQuestRoom> Rooms = new List<HeroQuestRoom>();

        protected List<HeroQuestPart> TempMap = new List<HeroQuestPart>();

        protected List<Point> FilledPositions = new List<Point>();

        public HeroQuestGenerator()
        {
            HeroQuestRoom LargeRoom = new HeroQuestRoom();
            LargeRoom.GridSize = new Size(10, 5);
            Rooms.Add(LargeRoom);

            HeroQuestRoom SmallRoom = new HeroQuestRoom();
            SmallRoom.GridSize = new Size(5, 5);
            Rooms.Add(SmallRoom);

            HeroQuestPassage LongPassage = new HeroQuestPassage();
            LongPassage.GridSize = new Size(2, 10);
            Passages.Add(LongPassage);

            HeroQuestPassage ShortPassage = new HeroQuestPassage();
            ShortPassage.GridSize = new Size(2, 5);
            Passages.Add(ShortPassage);

            HeroQuestPassage DeadEnd = new HeroQuestPassage();
            DeadEnd.GridSize = new Size(2, 5);
            DeadEnd.DeadEnd = true;
            Passages.Add(DeadEnd);

            HeroQuestPassage Stairs = new HeroQuestPassage();
            Stairs.GridSize = new Size(2, 2);
            Stairs.DeadEnd = true;
            Stairs.Stairs = true;
            Passages.Add(Stairs);

            HeroQuestConnector Corner = new HeroQuestConnector();
            Corner.Corner = true;
            Connectors.Add(Corner);

            HeroQuestConnector T = new HeroQuestConnector();
            T.Corner = false;
            Connectors.Add(T);
        }

        protected bool PartIsClear ( HeroQuestPart part )
        {
            lock (FilledPositions)
            {
                Rectangle rect = part.GetRect();
                for ( int y = rect.Top; y <= rect.Bottom; y++)
                {
                    for ( int x = rect.Left; x <= rect.Right; x++ )
                    {
                        if (FilledPositions.Contains(new Point(x,y)))
                            return false;
                    }
                }
            }

            return true;
        }

        protected void StampPart ( HeroQuestPart part )
        {
            lock (FilledPositions)
            {
                Rectangle rect = part.GetRect();
                for ( int y = rect.Top; y <= rect.Bottom; y++)
                {
                    for ( int x = rect.Left; x <= rect.Right; x++ )
                        FilledPositions.Add(new Point(x,y));
                }
            }
        }

        public Map Generate()
        {
            Map map = new Map();

            HeroQuestPassage Stairs = new HeroQuestPassage();
            Stairs.GridSize = new Size(2, 4);
            Stairs.DeadEnd = true;
            Stairs.Stairs = true;
            Stairs.Start = true;

            Stairs.Origin = new Point(0, 0);

            TempMap.Add(Stairs);

            return map;
        }

        protected void ProcessPassageway ( HeroQuestPassage passage )
        {

        }
    }
}
