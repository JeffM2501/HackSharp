using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WData
{
    public enum WorldDirection
    {
        North = 0,
        South = 1,
        East = 2,
        West = 3,
        Up = 4,
        Down = 5,
    }

    public class DirectionUtils
    {
        public static WorldDirection GetFacingDir(WorldDirection dir)
        {
            switch (dir)
            {
                case WorldDirection.Up:
                    return WorldDirection.Down;
                case WorldDirection.Down:
                    return WorldDirection.Up;
                case WorldDirection.North:
                    return WorldDirection.South;
                case WorldDirection.South:
                    return WorldDirection.North;
                case WorldDirection.East:
                    return WorldDirection.West;
            }
            return WorldDirection.East;
        }
    }
    
    public enum Solidity
    {
        Empty,
        Solid,
        Half,
        Quarter,
        ThreeQuarter,
    }

//     public enum SlopeDirection
//     {
//         North,
//         South,
//         East,
//         West
//     }

//     public enum CutCorner
//     {
//         None,
//         NorthEast,
//         NorthWest,
//         SouthEast,
//         SouthWest,
//     }

    public class Block
    {
        public Solidity Height = Solidity.Empty;
     //   public Solidity Slope = Solidity.Empty; // ignored if height is full
     //   public SlopeDirection Direction = SlopeDirection.North; // ignored if height is full or slope is empty
     //   public CutCorner Corner = CutCorner.None; // ignored if slope is not empty

        public int[] Materials = new int[6] { -1, -1, -1, -1, -1, -1 };

        public Block Clone ( )
        {
            Block newBlock = new Block();
            newBlock.Height = Height;
//             newBlock.Direction = Direction;
//             newBlock.Direction = Direction;
//             newBlock.Corner = Corner;
            newBlock.Materials = Materials;
            newBlock.Tag = Tag;
            return newBlock;
        }

        public void SetMaterials(int id)
        {
            for (int i = 0; i < Materials.Length; i++)
                Materials[i] = id;
        }

        public object Tag = null;
    }

    public class Cluster
    {
        static Dictionary<int, Cluster> World = new Dictionary<int, Cluster>();

        public static Dictionary<int, Cluster> Map
        {
            get { return World; }
        }

        public int ID;
        public static int XYSize = 16;
        public static int ZSize = 64;

        public int OriginX = 0;
        public int OriginY = 0;
        public int OriginZ = 0;

        // north, south, east, west, up, down
        public int[] Neighbors = new int[6] { -1, -1, -1, -1, -1, 1 };

        public object Tag = null;
        public object MapTag = null;
        public object RenderTag = null;

        public class Plane
        {
            public Block[,] Members = null;
            public Block Solid = null; // valid if the plane is entirely one block type;

            public bool SideSolid(WorldDirection dir)
            {
                if (Solid != null && Solid.Height != Solidity.Empty)// it's full of empty blocks
                    return true;

                if (dir == WorldDirection.North)
                {
                    for (int x = 0; x < Cluster.XYSize; x++)
                    {
                        if (Members[x, Cluster.XYSize - 1].Height != Solidity.Solid)
                            return false;
                    }
                    return true;
                }

                if (dir == WorldDirection.South)
                {
                    for (int x = 0; x < Cluster.XYSize; x++)
                    {
                        if (Members[x, 0].Height != Solidity.Solid)
                            return false;
                    }
                    return true;
                }

                if (dir == WorldDirection.East)
                {
                    for (int y = 0; y < Cluster.XYSize; y++)
                    {
                        if (Members[Cluster.XYSize-1, y].Height != Solidity.Solid)
                            return false;
                    }
                    return true;
                }

                if (dir == WorldDirection.West)
                {
                    for (int y = 0; y < Cluster.XYSize; y++)
                    {
                        if (Members[0, y].Height != Solidity.Solid)
                            return false;
                    }
                    return true;
                }

                for (int x = 0; x < Cluster.XYSize; x++)
                {
                    for (int y = 0; y < Cluster.XYSize; y++)
                    {
                        if (Members[x, y].Height != Solidity.Solid)
                            return false;
                    }
                }
                return true; // it has to be solid
            }

            public void FillFromSolid ( )
            {
                if (Solid == null)
                    return;

                Members = new Block[XYSize, XYSize];

                for(int x = 0; x < XYSize; x++)
                {
                    for (int y = 0; y < XYSize; y++)
                        Members[x, y] = Solid.Clone();
                }
                Solid = null;
            }
        }

        public Plane[] Planes = new Plane[ZSize];

        public bool Contains ( int X, int Y, int Z )
        {
            if (Z < OriginZ || Z >= OriginZ + ZSize)
                return false;
            if (X < OriginX || X >= OriginX + XYSize)
                return false;
            if (Y < OriginY || Y >= OriginY + XYSize)
                return false;

            return true;
        }

        public Block GetBlock ( int X, int Y, int Z )
        {
            return Planes[Z - OriginZ].Members[X - OriginX, Y - OriginY];
        }

        public void LinkNeighbors()
        {
            foreach ( KeyValuePair<int,Cluster> c in World)
            {
                if (c.Value != this)
                {
                    if (c.Value.OriginX == OriginX + 1 && c.Value.OriginY == OriginY)
                        Neighbors[(int)WorldDirection.East] = c.Key;

                    if (c.Value.OriginX == OriginX - 1 && c.Value.OriginY == OriginY)
                        Neighbors[(int)WorldDirection.West] = c.Key;

                    if (c.Value.OriginX == OriginX && c.Value.OriginY == OriginY + 1)
                        Neighbors[(int)WorldDirection.North] = c.Key;

                    if (c.Value.OriginX == OriginX && c.Value.OriginY == OriginY - 1)
                        Neighbors[(int)WorldDirection.South] = c.Key;
                }
            }
        }

        public Plane GetNeighbor(int Z, WorldDirection dir)
        {
            int neighbor = Neighbors[(int)dir];
            if (neighbor == -1)
                return null;

            return World[neighbor].Planes[Z];
        }

        public Block GetNeighbor(int X, int Y, int Z, WorldDirection dir)
        {
            int localX = X - OriginX;
            int localY = Y - OriginY;
            int localZ = Z - OriginZ;

            int neighbor = Neighbors[(int)dir];

            switch (dir)
            {
                case WorldDirection.North:
                    if (localY == XYSize - 1)
                    {
                        if ( neighbor == -1)
                            return null;
                        
                        return World[neighbor].GetBlock(X,Y+1,Z);
                    }
                    return GetBlock(X,Y+1,Z);

                case WorldDirection.South:
                    if (localY == 0)
                    {
                        if ( neighbor == -1)
                            return null;
                        
                        return World[neighbor].GetBlock(X,Y-1,Z);
                    }
                    return GetBlock(X,Y-1,Z);

                case WorldDirection.East:
                    if (localX == XYSize - 1)
                    {
                        if ( neighbor == -1)
                            return null;
                        
                        return World[neighbor].GetBlock(X+1,Y,Z);
                    }
                    return GetBlock(X+1,Y,Z);

                case WorldDirection.West:
                    if (localX == 0)
                    {
                        if ( neighbor == -1)
                            return null;
                        
                        return World[neighbor].GetBlock(X-1,Y,Z);
                    }
                    return GetBlock(X-1,Y,Z);

                case WorldDirection.Up:
                    if (localZ == ZSize - 1)
                    {
                        if ( neighbor == -1)
                            return null;
                        
                        return World[neighbor].GetBlock(X,Y,Z+1);
                    }
                    return GetBlock(X,Y,Z+1);

                case WorldDirection.Down:
                    if (localZ == 0)
                    {
                        if ( neighbor == -1)
                            return null;
                        
                        return World[neighbor].GetBlock(X,Y,Z-1);
                    }
                    return GetBlock(X,Y,Z-1);
            }
            return null;
        }
    }
}
