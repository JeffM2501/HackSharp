using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WData;

namespace SharpShooter
{
    class World
    {
        public enum BlockType
        {
            Air,
            Dirt,
            Grass,
            Stone,
        }

        public static int StoneTextureID = 1;
        public static int DirtTextureID = 2;
        public static int GrassTopTextureID = 3;
        public static int GrassSideTextureID = 4;

        public Dictionary<int, Cluster> Map = new Dictionary<int, Cluster>();

        protected int LastID = 1;

        public World()
        {
            Map = Cluster.Map;
        }

        public void GenerateSimple()
        {
            int h = 5;

            int groundHeight = 32;
            int stoneHeight = 20;

            int startID = LastID;

            Block stone = new Block();
            stone.Tag = BlockType.Stone;
            stone.Height = Solidity.Solid;
            stone.SetMaterials(StoneTextureID);
   
            Block dirt = new Block();
            dirt.Tag = BlockType.Dirt;
            dirt.Height = Solidity.Solid;
            dirt.SetMaterials(DirtTextureID);

            Block grass = new Block();
            grass.Tag = BlockType.Grass;
            grass.Height = Solidity.Solid;
            grass.SetMaterials(GrassSideTextureID);
            grass.Materials[4] = GrassTopTextureID;
            grass.Materials[5] = DirtTextureID;

            Block air = new Block();
            air.Tag = BlockType.Air;
            air.Height = Solidity.Empty;

            for (int y = -h; y < h; y++)
            {
                for (int x = -h; x < h; x++)
                {
                    Cluster c = new Cluster();
                    c.OriginX = x * Cluster.XYSize;
                    c.OriginY = y * Cluster.XYSize;
                    c.OriginZ = 0;
                    c.ID = LastID++;

                    for (int z = 0; z < Cluster.ZSize; z++)
                    {
                        Cluster.Plane plane = new Cluster.Plane();
                        if (z < stoneHeight)
                            plane.Solid = stone;
                        else if (z < groundHeight)
                            plane.Solid = dirt;
                        else if (z == groundHeight)
                            plane.Solid = grass;
                        else
                            plane.Solid = air;

                        c.Planes[z] = plane;
                    }
                    Map.Add(c.ID, c);
                }
            }

            foreach (KeyValuePair<int, Cluster> c in Map)
                c.Value.LinkNeighbors();
        }
    }
}
