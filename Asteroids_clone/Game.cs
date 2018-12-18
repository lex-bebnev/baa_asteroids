using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;


namespace Asteroids_clone
{
    public sealed class Game: GameWindow
    {

        private float[] vertices =
        {
            /*0.5f,  0.5f, 0.0f, // top right
            0.5f, -0.5f, 0.0f, // bottom right
            -0.5f, -0.5f, 0.0f, // bottom left
            -0.5f,  0.5f, 0.0f, // top left*/
            0.0f, 0.0f, -0.05f, // center
            -0.05f, 0.2f, 0.0f, // top left
            -0.2f, -0.2f, -0.05f, // bottom left
            0.2f, -0.05f, 0.0f // bottom right
        };
        
        Vector3[] vertdata = new Vector3[] { 
            new Vector3(-0.8f, -0.8f,  -0.8f),
            new Vector3(0.8f, -0.8f,  -0.8f),
            new Vector3(0.8f, 0.8f,  -0.8f),
            new Vector3(-0.8f, 0.8f,  -0.8f),
            new Vector3(-0.8f, -0.8f,  0.8f),
            new Vector3(0.8f, -0.8f,  0.8f),
            new Vector3(0.8f, 0.8f,  0.8f),
            new Vector3(-0.8f, 0.8f,  0.8f),
        };
        
       
        private Vector3[] modelPositions = new[]
        {
            new Vector3(5.0f, 5.0f, 0.0f),
            /*new Vector3(-5.0f, -5.0f, 0.0f),*/
        };
        
        Vector3 cameraPostiton = new Vector3(0.0f, 0.0f, 3.0f);
        
        uint[] indices =
        {
            //Note that indices start at 0!
            0, 2, 1, //The first triangle will be the bottom-right half of the triangle
            0, 2, 3  //Then the second will be the top-right half of the triangle
            
            /*//front
            0, 7, 3,
            0, 4, 7,
            //back
            1, 2, 6,
            6, 5, 1,
            //left
            0, 2, 1,
            0, 3, 2,
            //right
            4, 5, 6,
            6, 7, 4,
            //top
            2, 3, 6,
            6, 3, 7,
            //bottom
            0, 1, 5,
            0, 5, 4*/
        };
        
        
        
        private int VertexBufferObject;
        private int VertexArrayObject;
        Shader shader;
        
        private int ElementBufferObject;
        private int Degrees;
        private Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);
        
        Matrix4 view;
        Matrix4 projection;   
        
        public Game(int width, int height, string title): base(width, height, GraphicsMode.Default, title)
        {
            Console.WriteLine("Initialization...");
            Input.Initialize(this);
            RenderFrame += OnRenderFrame;
            UpdateFrame += OnUpdateFrame;
        }

        protected override void OnLoad(EventArgs e)
        {
            Color4 backColor = Color4.Black;
            GL.ClearColor(backColor);//0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); //See polygons
            
            //We need to send our vertices over to the graphics card so OpenGL can use them.
            //To do this, we need to create what's called a Vertex Buffer Object (VBO).
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ElementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            
            
            shader = new Shader("shader.vert", "shader.frag");
            shader.Use();
            
            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBufferObject);
            
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);

            view = Matrix4.LookAt(new Vector3(0.0f, 0.0f, -5.0f), Vector3.Zero, Vector3.Zero);

            projection = Matrix4.CreateOrthographic(Width, Height, 0.1f, 100f);//Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(50.0f), (float)Width / (float)Height, 0.1f, 100.0f);
            
            GL.Viewport(0,0,Width,Height);
            
            Degrees = 0;
            
            base.OnLoad(e);
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            Input.Update();
        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {         
            if (Input.KeyDown(Key.D))
            {
                Title = $"Pressed D!";
                Degrees += 1;
            }
            else
            if (Input.KeyDown(Key.A))
            {
                Title = $"Pressed A!";
                Degrees -= 1;
            }
            else if (Input.KeyDown(Key.W))
            {
                Title = $"Pressed W!";
                position.Y += 1;
            }
            else if (Input.KeyDown(Key.S))
            {
                Title = $"Pressed s!";
                position.Y -= 1;
            }
            
            else Title = $"(Vsync: {VSync}) FPS: {1f / e.Time:0}";
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            shader.Use();
            shader.SetMatrix4("projection", Matrix4.Identity);
            shader.SetMatrix4("view", Matrix4.Identity);


            Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationZ((float) MathHelper.DegreesToRadians(Degrees)) * 
                                              Matrix4.CreateTranslation(position);
            
            shader.SetMatrix4("model", model);

            GL.BindVertexArray(VertexArrayObject);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            
            SwapBuffers();
        }
        
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }
    }
}