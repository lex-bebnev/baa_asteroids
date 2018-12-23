using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

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

    public class ObjectParams
    {
        internal Vector3 Position;
        internal Vector3 Rotation;
        internal Vector3 Scale;

        public ObjectParams(float positionX, float positionY, float positionZ,
            float rotationX, float rotationY, float rotationZ,
            float scaleX, float scaleY, float scaleZ)
        {
            Position = new Vector3(positionX, positionY, positionZ);
            Rotation = new Vector3(rotationX, rotationY, rotationZ);
            Scale = new Vector3(scaleX, scaleY, scaleZ);
        }

        public void SetPosition(float positionX, float positionY, float positionZ)
        {
            Position.X = positionX;
            Position.Y = positionY;
            Position.Z = positionZ;
        }

        public void SetRotation(float rotationX, float rotationY, float rotationZ)
        {
            Rotation.X = rotationX;
            Rotation.Y = rotationY;
            Rotation.Z = rotationZ;
        }

        public void SetScale(float scaleX, float scaleY, float scaleZ)
        {
            Scale.X = scaleX;
            Scale.Y = scaleY;
            Scale.Z = scaleZ;
        }
    }
    
    public static class Renderer
    {
        public static Matrix4 ModelMatrix { get; set; }
        public static Matrix4 ViewMatrix { get; set; }
        public static Matrix4 ProjectionMatrix { get; set; }
        
        private static Shader shader;
        
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

        public static void SetupRenderer(GameWindow window)
        {
            SetViewport(0, 0, window.Width, window.Height);
            SetProjection(window.Width, window.Height);
            Color4 backColor = Color4.Black;
            GL.ClearColor(backColor);
            GL.Enable(EnableCap.DepthTest);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            shader = new Shader("shader.vert","shader.frag");
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
        public static void DrawTriangle(int vertexArrayObject, int size, ObjectParams parameters)
        {
            var t2 = Matrix4.CreateTranslation(parameters.Position);
            var r3 = Matrix4.CreateRotationZ(parameters.Rotation.Z);
            var s = Matrix4.CreateScale(parameters.Scale);
            Matrix4 _modelView = r3 * s * t2;
            
            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetMatrix4("model", _modelView);
            
            GL.DrawElements(PrimitiveType.Triangles, size, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);
        }

        /// <summary>
        ///     Draw line
        /// </summary>
        /// <param name="verteces">Array of vertces coordinate (x,y,z)</param>
        /// <param name="colors">Array of verteces color (r,g,b,a)</param>
        public static void DrawLine(float[] verteces, float[] colors)
        {
            throw new NotImplementedException();
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
        public static void RenderText(string text, float x, float y, float scale)
        {
            throw new NotImplementedException();
        }

        public static void RenderSprite(/*Sprite sprite*/)
        {
            throw new NotImplementedException();      
        }
        
        #endregion

        public static void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            
            shader.Dispose();
        }
    }
}