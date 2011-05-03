using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;

using OpenTK.Graphics;
using OpenTK;

namespace Renderer
{
    public class Display
    {
        public System.Windows.Forms.Control BaseControl = null;
        public OpenTK.GameWindow GameWin = null;

        protected Size Bounds = Size.Empty;

        public delegate void SimpleEvent(object sender, EventArgs args);
        public event SimpleEvent Resized;

        protected Color ClearColor = Color.DarkGray;
        public Color BackgroundColor
        {
            get
            {
                return ClearColor;
            }

            set
            {
                ClearColor = value;
                GL.ClearColor(ClearColor);
            }
        }
                

        public int Width
        {
            get
            {
                if (BaseControl == null)
                    return Bounds.Width;
                return BaseControl.Width;
            }
        }

        public int Height
        {
            get
            {
                if (BaseControl == null)
                    return Bounds.Height;
                return BaseControl.Height;
            }
        }

        public Display ( Size s )
        {
            Resize(s);
            Init();
        }

        public Display(GameWindow w)
        {
            GameWin = w;
            GameWin.Resize += new EventHandler<EventArgs>(GameWin_Resize);
            Resize(w.Bounds.Size);
            Init();
        }

        public Display ( Control c )
        {
            BaseControl = c;
            BaseControl.Resize +=new EventHandler(BaseControl_Resize);
            Init();
        }

        public void Resize ( Size s )
        {
            MakeCurrent();
            Bounds = s;
            GL.Viewport(0, 0, Bounds.Width, Bounds.Height); // Use all of the glControl painting area  
            if (Resized != null)
                Resized(this, EventArgs.Empty);
        }

        void  BaseControl_Resize(object sender, EventArgs e)
        {
            Resize(BaseControl.Bounds.Size);
        }

        void GameWin_Resize(object sender, EventArgs e)
        {
            Resize(GameWin.Bounds.Size);
        }

        public void MakeCurrent()
        {
            if (BaseControl as OpenTK.GLControl != null)
                ((OpenTK.GLControl)BaseControl).MakeCurrent();
            else if (GameWin != null)
                GameWin.MakeCurrent();
        }

        protected void Init()
        {
            GL.ClearColor(ClearColor);

            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.ShadeModel(ShadingModel.Smooth);
            GL.PolygonMode(MaterialFace.Front, PolygonMode.Fill);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.LineSmooth);
            GL.Enable(EnableCap.DepthTest);
        }

        public void Clear()
        {
            MakeCurrent();
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public void SwapBuffers()
        {
            if (BaseControl as OpenTK.GLControl != null)
                ((OpenTK.GLControl)BaseControl).SwapBuffers();
            else if (GameWin != null)
                GameWin.SwapBuffers();
        }
    }
}
