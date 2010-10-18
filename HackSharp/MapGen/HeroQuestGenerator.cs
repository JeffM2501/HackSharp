using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Maps;

namespace MapGen
{
    class HeroQuestPart
    {
        public class LinkNode
        {
            public HeroQuestPart Destination;
            public Point Location = Point.Empty;

            public LinkNode ()
            {

            }

            public LinkNode ( HeroQuestPart p, Point l)
            {
                Destination = p;
                Location = l;
            }
        }

        public virtual bool Room
        {
            get { return true; }
        }

        public int Rotation = 0;

        public Size GridSize = Size.Empty;
        public Point Origin = Point.Empty;

        public static int RandomAngle()
        {
            return new Random().Next(3) * 90;
        }

        public virtual Rectangle GetRect()
        {
            if (Rotation == 90)
                return new Rectangle(Origin.X,Origin.Y, GridSize.Height,GridSize.Width);
            else if (Rotation == 180)
                return new Rectangle(Origin.X, Origin.Y, -GridSize.Width, -GridSize.Height);
            if (Rotation == 270)
                return new Rectangle(Origin.X, Origin.Y, -GridSize.Height, -GridSize.Width);

            return new Rectangle(Origin, GridSize);
        }

        public Point TransformPos ( Point pos )
        {
            if (Rotation == 90)
                return new Point(Origin.X + pos.Y, Origin.Y - pos.Y);
            else if (Rotation == 180)
                return new Point(Origin.X - pos.X, Origin.Y - pos.Y);
            if (Rotation == 270)
                return new Point(Origin.X - pos.Y, Origin.Y + pos.X);

            return new Point(Origin.X + pos.X,Origin.Y + pos.Y);
        }

        public int RotateAngle ( int angle )
        {
            int a = Rotation + angle;
            while (a < 360)
                a -= 360;
            while (a > 0)
                a += 360;

            return a;
        }

        public List<HeroQuestDoor> Doors = new List<HeroQuestDoor>();

        public List<LinkNode> Links = new List<LinkNode>();

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

        protected Dictionary<Point, HeroQuestPart> FilledPositions = new Dictionary<Point, HeroQuestPart>();

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
                        if (FilledPositions.ContainsKey(new Point(x,y)))
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
                        FilledPositions.Add(new Point(x,y),part);
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

            HeroQuestPassage StartHall = new HeroQuestPassage();
            StartHall.GridSize = new Size(2, 10);

           // LinkNode l = new LinkNode()
           // StartHall.Links;
            TempMap.Add(StartHall);


            return map;
        }

        protected void ProcessPassageway ( HeroQuestPassage passage )
        {

        }
    }
}
