using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Vector3 = OpenTK.Vector3;
using AntTweakBar;
using QuickFont;
using QuickFont.Configuration;

namespace Asteroids_clone
{
    public sealed class Game: GameWindow
    {

        float[] shipVertices =
        {
            //Position          
             0.0f,   0.25f, 0.0f, 
            -0.25f,   0.4f, 0.0f, 
             0.25f,   0.4f, 0.0f, 
              0.0f,  -0.4f, 0.0f
        };
        uint[] shipIndices =
        {
            0, 1, 3,
            0, 3, 2
        };

        private float[] UfoVertecies =
        {
             -0.5f,  0.0f, 0.0f,
              0.5f,  0.0f, 0.0f,
            -0.25f, -0.2f, 0.0f,
             0.25f, -0.2f, 0.0f,
            -0.25f,  0.2f, 0.0f,
             0.25f,  0.2f, 0.0f,
            -0.05f,  0.5f, 0.0f,
             0.05f,  0.5f, 0.0f
        };
        private uint[] UfoIndeces =
        {
            0,1,2,
            1,2,3,
            0,4,1,
            4,5,1,
            4,6,7,
            4,5,7
        };

        float[] asteroidVertices =
        {        
            0.5f, 0.5f, 0.0f, 
            0.5f, -0.5f, 0.0f, 
            -0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.0f
        };
        uint[] asteroidIndices =
        {
            0, 1, 2,
            0, 2, 3
        };
        
        
        //private RenderableObject playerModel;
        //private RenderableObject UfoModel;
        //private RenderableObject asteroidModel;
        
        
        private int VertexBufferObject;
        private int VertexArrayObject;
        Shader shader;
        private int ElementBufferObject;
        private float Degrees;
        
        private Vector3 position = new Vector3(0.0f, 0.0f, -5.0f);
        
        Matrix4 view;
        Matrix4 projection;

        private Context context;
        private DoubleVariable FpsLabel;

        private QFontDrawing _drawing;
        private QFont _font;
        
        public Game(int width, int height, string title): base(width, height, GraphicsMode.Default, title)
        {
            
            
            Console.WriteLine("Initialization...");
            //InputManager.Initialize(this);

            Initialze();
            
            RenderFrame += OnRenderFrame;
            UpdateFrame += OnUpdateFrame;
        }

        private void Initialze()
        {
            //playerModel = new RenderableObject(shipVertices,shipIndices);
            //UfoModel = new RenderableObject(UfoVertecies, UfoIndeces);
            //asteroidModel = new RenderableObject(asteroidVertices, asteroidIndices);
        }

        protected override void OnLoad(EventArgs e)
        {
            context = new Context(Tw.GraphicsAPI.OpenGL);
            
            var configsBar = new Bar(context);
            configsBar.Label = "Configuration";
            configsBar.Contained = true;
            
            FpsLabel = new DoubleVariable(configsBar, 0.0d);
            FpsLabel.Label = "FPS";
            
            VSync = VSyncMode.On;
            Color4 backColor = Color4.Black;
            GL.ClearColor(backColor);//0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);/*
            GL.DepthFunc(DepthFunction.Less);
            GL.Enable(EnableCap.CullFace);*/
            /*GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);*/
            
            
            CreateProjection();
            
            _font = new QFont("/Fonts/HappySans.ttf", 24.0f, new QFontBuilderConfiguration(true));
            _drawing = new QFontDrawing();
            
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); //See polygons

            PrepareRenderModel();
            
            Degrees = 0.0f;
        }

        private void PrepareRenderModel()
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, shipVertices.Length * sizeof(float), shipVertices,
                BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, shipIndices.Length * sizeof(uint), shipIndices,
                BufferUsageHint.StaticDraw);

            shader = new Shader("shader.vert", "shader.frag");
            shader.Use();

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);

            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            //Input.Update();
        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {       
            /*if (Input.KeyDown(Key.D))
            {
                Title = $"Pressed D!";
                Degrees += 1.0f * (float) e.Time;
            }
            else
            if (Input.KeyDown(Key.A))
            {
                Title = $"Pressed A!";
                Degrees -= 1.0f * (float) e.Time;
            }else 
            if (Input.KeyDown(Key.W))
            {
                Title = $"Pressed W!";
                position.X += 1.0f * (float)  e.Time;
            }
            else if (Input.KeyDown(Key.S))
            {
                Title = $"Pressed s!";
                position.X -= 1.0f * (float)  e.Time;
            }*/
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color4.Black);
            
            _drawing.DrawingPrimitives.Clear();
            _drawing.Print(_font, "TEXT!!!", new Vector3(50.0f, 100.0f, -1.0f), QFontAlignment.Left);
            
            _drawing.RefreshBuffers();
            _drawing.ProjectionMatrix = projection;
            _drawing.Draw();
            GL.Disable(EnableCap.Blend);
            
            double fps = 1d / e.Time;
            FpsLabel.Value = fps;
            
            RenderModel();
            
            context.Draw();
            
            SwapBuffers();
        }

        private void RenderModel()
        {
            GL.BindVertexArray(VertexArrayObject);

            shader.Use();
            var t2 = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            var r3 = Matrix4.CreateRotationZ(Degrees);
            var s = Matrix4.CreateScale(100.0f);
            Matrix4 _modelView = r3 * s * t2;

            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("model", _modelView);

            GL.DrawElements(PrimitiveType.Triangles, shipIndices.Length, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(0);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            context.HandleResize(ClientSize);
            CreateProjection();
        }

        private void CreateProjection()
        {
            /*float fov = 60.0f;
            var aspect = Width / Height;
            projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fov), 
                aspect,
                0.1f, 
                4000.0f);
            */
            projection = Matrix4.CreateOrthographic((float)Width, (float)Height, 0.1f, 4000f);
        }
    }
}