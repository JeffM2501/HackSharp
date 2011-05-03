using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;
using OpenTK.Graphics;

namespace Renderer
{
    public class Camera
    {
        Display TheDisplay = null;
        bool Ortho = true;

        public int FOV = 45;
        public int PerspectiveNear = 0;
        public int PerspectiveFar = 1000;

        public int OrthoNear = 0;
        public int OrthoFar = 10;

        public Vector3 ViewPosition = Vector3.Zero;
        public double Spin = 0;
        public double Tilt = 0;
        public double Pullback = 0;

        public Camera(Display display)
        {
            TheDisplay = display;
            TheDisplay.Resized += new Display.SimpleEvent(TheDisplay_Resized);
        }

        public void Reset()
        {
            TheDisplay_Resized(this, EventArgs.Empty);
        }

        void TheDisplay_Resized(object sender, EventArgs args)
        {
            if (Ortho)
                SetOrthographic();
            else
                SetPerspective();
        }

        public void SetPerspective()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float aspect = (float)TheDisplay.Width / (float)TheDisplay.Height;

            Glu.Perspective(FOV, aspect, PerspectiveNear, PerspectiveFar);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void SetOrthographic()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, TheDisplay.Width, 0, TheDisplay.Height, OrthoNear, OrthoFar);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void Execute()
        {
            if (!Ortho)
            {
                SetPerspective();
                GL.LoadIdentity();

                GL.Translate(0, 0, -Pullback);						// pull back on along the zoom vector
                GL.Rotate(Tilt, 1.0f, 0.0f, 0.0f);					// pops us to the tilt
                GL.Rotate(-Spin, 0.0f, 1.0f, 0.0f);					// gets us on our rot
                GL.Translate(-ViewPosition.X, -ViewPosition.Z, ViewPosition.Y);	                        // take us to the pos
                GL.Rotate(-90, 1.0f, 0.0f, 0.0f);				    // gets us into XY
            }
            else
            {
                SetOrthographic();
                GL.LoadIdentity();
            }
        }

        public void ExecuteBillboard()
        {
            GL.Rotate(Spin, 0, 0, 1);
            GL.Rotate(-Tilt, 1, 0, 0);
        }
    }
}
