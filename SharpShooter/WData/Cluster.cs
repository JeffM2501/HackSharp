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

        public int[] Materials = new int[6]{0,0,0,0,0,0};

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

        public object Tag = null;
    }

    public class Cluster
    {
        static Dictionary<int, Cluster> World = new Dictionary<int, Cluster>();

        public int ID;
        public static int XYSize = 16;
        public static int ZSize = 64;

        public int OriginX = 0;
        public int OriginY = 0;
        public int OriginZ = 0;

        public int[] Neighbors = new int[6] { -1, -1, -1, -1, -1, 1 };

        public object Tag = null;

        public class Plane
        {
            public Block[,] Members = null;
            public Block Solid = null; // valid if the plane is entirely one block type;

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
