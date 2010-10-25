using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Textures;

using WData;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SharpShooter
{
    class WorldRenderer
    {
        public Dictionary<int, Texture> MaterialTextureMap = new Dictionary<int, Texture>();

        public class ClusterRenderer
        {
            public void Update(Cluster cluster)
            {

            }

            public void Draw()
            {

            }
        }

        public class CameraView
        {
            public Vector3 Position = Vector3.Zero;
            public double Spin = 0;
            public double Tilt = 0;

            public void Execute()
            {
                GL.Rotate(Tilt, 1.0f, 0.0f, 0.0f);					// pops us to the tilt
                GL.Rotate(-Spin, 0.0f, 1.0f, 0.0f);					// gets us on our rot
                GL.Translate(-Position.X, -Position.Z, Position.Y); // take us to the pos
                GL.Rotate(-90, 1.0f, 0.0f, 0.0f);				    // gets us into XY
            }
        }

        Dictionary<int, Cluster> Map;

        public WorldRenderer(Dictionary<int, Cluster> world)
        {
            Map = world;
        }

        public void InitRenderData()
        {
            foreach (KeyValuePair<int, Cluster> c in Map)
                SetRenderData(c.Value);
        }

        public void SetRenderData(Cluster cluster)
        {
            if (cluster.RenderTag == null)
                cluster.RenderTag = new ClusterRenderer();

            (cluster.RenderTag as ClusterRenderer).Update(cluster);
        }

        public void DrawGrid()
        {
            GL.Color4(Color.Wheat);

            GL.Begin(BeginMode.Lines);
            for (int i = 0; i < 25; i++)
            {
                GL.Vertex3(-25, -i, 0);
                GL.Vertex3(25, -i, 0);

                GL.Vertex3(-25, i, 0);
                GL.Vertex3(25, i, 0);

                GL.Vertex3(-i, 25, 0);
                GL.Vertex3(-i, -25, 0);

                GL.Vertex3(i, -25, 0);
                GL.Vertex3(i, 25, 0);
            }
            GL.End();
        }

        public void Draw(CameraView view)
        {
            GL.PushMatrix();
            view.Execute();

            DrawGrid();

            foreach (KeyValuePair<int, Cluster> cluster in Map)
            {
                ClusterRenderer r = (cluster.Value.RenderTag as ClusterRenderer);
                if (r != null)
                    r.Draw();
            }

            GL.PopMatrix();
        }
    }
}
