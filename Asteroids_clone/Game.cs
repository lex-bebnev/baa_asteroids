using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using Vector3 = OpenTK.Vector3;


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
            VSync = VSyncMode.Off;
            Color4 backColor = Color4.Black;
            GL.ClearColor(backColor);//0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            CreateProjection();
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); //See polygons

            
            //We need to send our vertices over to the graphics card so OpenGL can use them.
            //To do this, we need to create what's called a Vertex Buffer Object (VBO).
            //PrepareRenderModel(UfoModel);

            Degrees = 0.0f;
        }

        /*private void PrepareRenderModel(RenderableObject model)
        {
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, model.GetVerticiesSize(), model.Verticies,
                BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, model.GetIndecesSize(), model.Indices,
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
        }*/

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

            //RenderModel(UfoModel);
            
            SwapBuffers();
        }

        /*private void RenderModel(RenderableObject model)
        {
            GL.BindVertexArray(VertexArrayObject);

            shader.Use();
            var t2 = Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            var r3 = Matrix4.CreateRotationZ(Degrees);
            var s = Matrix4.CreateScale(0.5f);
            Matrix4 _modelView = r3 * s * t2;

            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("model", _modelView);

            GL.DrawElements(PrimitiveType.Triangles, model.Indices.Length, DrawElementsType.UnsignedInt, 0);

            GL.BindVertexArray(0);
        }*/

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            CreateProjection();
        }

        private void CreateProjection()
        {
            float fov = 60.0f;
            var aspect = Width / Height;
            projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fov), 
                aspect,
                0.1f, 
                4000.0f);
        }
    }
}