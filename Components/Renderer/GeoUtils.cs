using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using OpenTK;
using OpenTK.Graphics;

namespace Renderer
{
    public class GeoUtils
    {
        public static void Quad(Point center, Size bounds, float z, Color color)
        {
            int w = bounds.Width/2;
            int h = bounds.Height/2;

            GL.Color4(color);
            GL.Begin(BeginMode.Quads);
            GL.Normal3(Vector3.UnitZ);
            GL.TexCoord2(0, 0);
            GL.Vertex3(center.X - w, center.Y - h,z);
            GL.TexCoord2(1, 0);
            GL.Vertex3(center.X + w, center.Y - h, z);
            GL.TexCoord2(1, 1);
            GL.Vertex3(center.X + w, center.Y + h, z);
            GL.TexCoord2(0, 1);
            GL.Vertex3(center.X - w, center.Y + h, z);
            GL.End();
        }

        public static void Quad(Point center, Size bounds, Color color)
        {
            GeoUtils.Quad(center, bounds,0, color);
        }

        public static void Quad(Point center, Size bounds)
        {
            GeoUtils.Quad(center, bounds, Color.White);
        }

        public static void Quad(Point center, Size bounds)
        {
            GeoUtils.Quad(center, bounds, Color.White);
        }

        public static void Quad(Point min, Point max, Color color)
        {

        }

    }
}
