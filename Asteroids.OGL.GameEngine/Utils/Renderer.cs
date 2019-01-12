using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using QuickFont;
using QuickFont.Configuration;

namespace Asteroids.OGL.GameEngine.Utils
{
    public struct LoadResult
    {
        public int VBO;
        public int EBO;
        public int VAO;

        public LoadResult(int vbo, int ebo, int vao)
        {
            VBO = vbo;
            EBO = ebo;
            VAO = vao;
        }
    }

    public static class Renderer
    {
        public static Matrix4 ModelMatrix { get; set; }
        public static Matrix4 ViewMatrix { get; set; }
        public static Matrix4 ProjectionMatrix { get; set; }
        
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        
        private static Shader shader;
        private static Shader spriteShader;
        
        private static QFontDrawing _drawing;
        private static QFont _font;
        
        public static void SetViewport(int x, int y, int width, int height)
        {
            GL.Viewport(x, y, width, height);
        }

        public static void SetProjection(int width, int height)
        {
            float fov = 60.0f;
            var aspect = width / height;
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fov), 
                aspect,
                0.1f, 
                4000.0f);
        }

        public static void SetOrthographic()
        {
            ProjectionMatrix = Matrix4.CreateOrthographic((float)Width, (float)Height, 0.1f, 4000f);
        }
        
        public static void SetupRenderer(GameWindow window)
        {
            Width = window.Width;
            Height = window.Height;
            SetViewport(0, 0, window.Width, window.Height);

            window.VSync = VSyncMode.On;
            
            SetOrthographic();
            Color4 backColor = Color4.Black;
            GL.ClearColor(backColor);
            GL.Enable(EnableCap.DepthTest); 
            
            GL.PointSize(10.0f);
            shader = new Shader("shader.vert", "shader.frag");
            spriteShader = new Shader("spriteShader.vert", "spriteShader.frag");

            _font = new QFont("/Fonts/HappySans.ttf", 14.0f, new QFontBuilderConfiguration());
            _drawing = new QFontDrawing();
        }

        /// <summary>
        ///     Load object mesh in GPU
        /// </summary>
        /// <param name="vertices">Vertices of object</param>
        /// <param name="indices">Indexes for construct model using trialngles</param>
        /// <returns></returns>
        public static LoadResult LoadObject(float[] vertices, uint[] indices)
        {
            Console.WriteLine("Load new object...");
            int vertexArrayObject = GL.GenVertexArray(); // Must be the first, otherwise there will be an error during the render phase
            int vertexBufferObject = GL.GenBuffer();
            int elementBufferObject = GL.GenBuffer();
            
            // VAO            
            GL.BindVertexArray(vertexArrayObject);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            
            shader.Use();
            
            int vertexLocation = shader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
            
            Console.WriteLine("Load new object complete");
            return new LoadResult(vertexBufferObject, elementBufferObject, vertexArrayObject);
        }

        public static LoadResult LoadSprite(float[] vertices, uint[] indices, Texture texture)
        {
            Console.WriteLine("Load new sprite...");
            int vertexArrayObject = GL.GenVertexArray(); // Must be the first, otherwise there will be an error during the render phase
            int vertexBufferObject = GL.GenBuffer();
            int elementBufferObject = GL.GenBuffer();
            
            // VAO            
            GL.BindVertexArray(vertexArrayObject);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            
            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
            
            spriteShader.Use();
            texture.Use();
            
            int vertexLocation = spriteShader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            
            int texCoordLocation = spriteShader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
            
            
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            
            Console.WriteLine("Load new sprite complete");
            return new LoadResult(vertexBufferObject, elementBufferObject, vertexArrayObject);
        }
        
        public static void UnloadObject(int vertexBufferObject, int vertexArrayObject)
        {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            // Delete all the resources.
            GL.DeleteBuffer(vertexBufferObject);
            GL.DeleteVertexArray(vertexArrayObject);
            shader.Dispose();
        }
        
        #region Draw tools

        /// <summary>
        ///     Draw triangle
        /// </summary>
        /// <param name="vertexArrayObject"></param>
        /// <param name="size"></param>
        /// <param name="postiton"></param>
        /// <param name="rotation"></param>
        /// <param name="scale"></param>
        public static void DrawTriangle(int vertexArrayObject, int size, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            var t2 = Matrix4.CreateTranslation(postiton);
            var r3 = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            var s = Matrix4.CreateScale(scale);
            Matrix4 modelView = s * r3 * t2;

            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetMatrix4("model", modelView);
            shader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -1.0f));
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            
            GL.DrawElements(PrimitiveType.Triangles, size, DrawElementsType.UnsignedInt, 0);
           
            GL.BindVertexArray(0);
        }

        /// <summary>
        ///     Draw line
        /// </summary>
        /// <param name="verteces">Array of vertces coordinate (x,y,z)</param>
        /// <param name="colors">Array of verteces color (r,g,b,a)</param>
        public static void DrawLineLoop(int vertexArrayObject, int size, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            var t2 = Matrix4.CreateTranslation(postiton);
            var r3 = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            var s = Matrix4.CreateScale(scale);
            Matrix4 modelView = s * r3 * t2;

            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetMatrix4("model", modelView);
            shader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -1.0f));
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            
            GL.DrawElements(PrimitiveType.LineLoop, size, DrawElementsType.UnsignedInt, 0);
           
            GL.BindVertexArray(0);
        }

        /// <summary>
        ///     Draw point 
        /// </summary>
        /// <param name="vertexArrayObject">Point VAO</param>
        /// <param name="postiton">pount position</param>
        /// <param name="rotation">Point rotation</param>
        /// <param name="scale">Point scale</param>
        public static void DrawPoint(int vertexArrayObject, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            var t1 = Matrix4.CreateTranslation(Vector3.Zero);
            var t2 = Matrix4.CreateTranslation(postiton);
            var r3 = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));//(float)(2 * Math.PI * (rotation.Z / 360)));
            var s = Matrix4.CreateScale(1f);
            Matrix4 _modelView = s * r3 * t2;// * r3;//r3 * s * t2;
            //_modelView *= t2;
            
            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetMatrix4("model", _modelView);
            shader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -10.0f));
            
            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            
            GL.BindVertexArray(0);
        }
        
        #endregion

        #region Render tools

        /// <summary>
        ///     Render text on screen
        /// </summary>
        /// <param name="text">Text for reder</param>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        /// <param name="scale">Font scale</param>
        public static void RenderText(string text, Vector3 position, float scale)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            
            _drawing.DrawingPrimitives.Clear();
            _drawing.Print(_font, text, position, QFontAlignment.Left);           
            _drawing.RefreshBuffers();
            
            _drawing.ProjectionMatrix = ProjectionMatrix;
            _drawing.Draw();
            
            GL.Disable(EnableCap.Blend);
        }
        
        /// <summary>
        ///     Render sprite on screen
        /// </summary>
        public static void RenderSprite(int vertexArrayObject, int size, Texture texture, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            var t2 = Matrix4.CreateTranslation(postiton);
            var r3 = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            var s = Matrix4.CreateScale(scale);
            Matrix4 modelView = s * r3 * t2;

            spriteShader.Use();
            texture.Use();

            GL.BindVertexArray(vertexArrayObject);
            spriteShader.SetMatrix4("projection", ProjectionMatrix);
            spriteShader.SetMatrix4("model", modelView);
            spriteShader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -1.0f));
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
            GL.DrawElements(PrimitiveType.Triangles, size, DrawElementsType.UnsignedInt, 0);
           
            GL.BindVertexArray(0);
            GL.Disable(EnableCap.Blend);
        }
        
        #endregion

        public static void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            
            shader.Dispose();
            spriteShader.Dispose();
            _drawing.Dispose();
            _font.Dispose();
        }
    }
}