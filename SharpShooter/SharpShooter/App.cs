using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using OpenTK;
using OpenTK.Graphics;

using WData;

using Textures;
using Resources;

namespace SharpShooter
{
    class App
    {
        protected GLControl Control;

        World Map = new World();

        WorldRenderer Renderer;
        WorldRenderer.CameraView View = new WorldRenderer.CameraView();

        public App(Form1 form)
        {
            form.Loaded += new Form1.NonTimeEvent(form_Load);
            form.Think += new Form1.TimeEvent(form_Update);
            form.Draw += new Form1.TimeEvent(form_Draw);
            form.Control.Resize += new EventHandler(Control_Resize);

            Control = form.Control;
        }

        void Control_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Control.Width, Control.Height); // Use all of the glControl painting area  
        }

        void form_Draw(double Now, double Delta)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            SetPerspective();
            GL.LoadIdentity();

            if (Renderer != null)
                Renderer.Draw(View);
        }

        void form_Update(double Now, double Delta)
        {
            
        }

        protected void SetPerspective()
        {
            double FOV = 60;

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            float aspect = (float)Control.Width / (float)Control.Height;

            Glu.Perspective(FOV, aspect, 0.1, 1000);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected void SetOrthographic()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Control.Width, 0, Control.Height, 0.01, 1000);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        void form_Load()
        {
            GL.ClearColor(Color.SkyBlue);

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

            // setup light 0
            Vector4 lightInfo = new Vector4(0.5f, 0.5f, 0.5f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Ambient, lightInfo);

            lightInfo = new Vector4(0.7f, 0.7f, 0.7f, 1.0f);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightInfo);
            GL.Light(LightName.Light0, LightParameter.Specular, lightInfo);

            Control_Resize(Control,EventArgs.Empty);

            LoadMap();
        }

        protected void LoadMap()
        {
            // load the world

            Map.GenerateSimple();
            // load it into the renderer
            Renderer = new WorldRenderer(Map.Map);
            Renderer.MaterialTextureMap.Add(World.StoneTextureID, Texture.Get(ResourceManager.FindFile("Data/terrain/stone.png"), true, true));
            Renderer.MaterialTextureMap.Add(World.DirtTextureID, Texture.Get(ResourceManager.FindFile("Data/dirt/stone.png"), true, true));
            Renderer.MaterialTextureMap.Add(World.GrassSideTextureID, Texture.Get(ResourceManager.FindFile("Data/terrain/grass_side.png"), true, true));
            Renderer.MaterialTextureMap.Add(World.GrassTopTextureID, Texture.Get(ResourceManager.FindFile("Data/terrain/grass.png"), true, true));

            View.Position = new Vector3(0, 0, 2);
        }
    }
}
