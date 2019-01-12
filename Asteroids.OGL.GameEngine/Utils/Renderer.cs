using System;
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
        public static Matrix4 ProjectionMatrix { get; set; }
        
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        
        private static Shader Shader;
        private static Shader SpriteShader;
        
        private static QFontDrawing Drawing;
        private static QFont Font;

        private static void SetupDisplaySettings(GameWindow window)
        {
            Width = window.Width;
            Height = window.Height;
            SetViewport(0, 0, Width, Height);
            window.VSync = VSyncMode.On;
        }

        private static void SetViewport(int x, int y, int width, int height)
        {
            GL.Viewport(x, y, width, height);
        }

        private static void SetOrthographic()
        {
            ProjectionMatrix = Matrix4.CreateOrthographic(Width, Height, 0.1f, 4000f);
        }

        private static void SetupBackground()
        {
            Color4 backColor = Color4.Black;
            GL.ClearColor(backColor);
            GL.Enable(EnableCap.DepthTest);
        }

        private static void SetupRenderTextUtils()
        {
            Font = new QFont("/Fonts/times.ttf", 14.0f, new QFontBuilderConfiguration());
            Drawing = new QFontDrawing();
        }

        private static void SetupShaders()
        {
            Shader = new Shader("polygonShader.vert", "polygonShader.frag");
            SpriteShader = new Shader("SpriteShader.vert", "SpriteShader.frag");
        }

        private static LoadResult GpuLoadData(float[] vertices, uint[] indices)
        {
            int vertexArrayObject =
                GL.GenVertexArray(); // Must be the first, otherwise there will be an error during the render phase
            int vertexBufferObject = GL.GenBuffer();
            int elementBufferObject = GL.GenBuffer();

            // VAO            
            GL.BindVertexArray(vertexArrayObject);

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
                BufferUsageHint.StaticDraw);

            // EBO
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices,
                BufferUsageHint.StaticDraw);

            return new LoadResult(vertexBufferObject, elementBufferObject, vertexArrayObject);
        }

        private static void ClearGpuBindedData()
        {
            GL.EnableVertexAttribArray(0);
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private static Matrix4 GetModelViewMatrix(Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            Matrix4 t2 = Matrix4.CreateTranslation(postiton);
            Matrix4 r3 = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            Matrix4 s = Matrix4.CreateScale(scale);
            Matrix4 modelView = s * r3 * t2;
            return modelView;
        }

        private static void SetShaderData(int vertexArrayObject, Matrix4 modelView, Shader shader)
        {
            shader.Use();
            GL.BindVertexArray(vertexArrayObject);
            shader.SetMatrix4("projection", ProjectionMatrix);
            shader.SetMatrix4("model", modelView);
            shader.SetMatrix4("view", Matrix4.CreateTranslation(0.0f, 0.0f, -1.0f));
        }
        
        public static void SetupRenderer(GameWindow window)
        {
            SetupDisplaySettings(window);
            SetOrthographic();
            SetupBackground(); 
            SetupShaders();
            SetupRenderTextUtils();
        }

        public static LoadResult LoadObject(float[] vertices, uint[] indices)
        {
            Console.WriteLine("Load new polygon object...");
            
            LoadResult gpuBindData = GpuLoadData(vertices, indices);
            
            Shader.Use();
            
            int vertexLocation = Shader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            
            ClearGpuBindedData();

            Console.WriteLine("Load new polygon object complete");
            
            return gpuBindData;
        }

        public static LoadResult LoadSprite(float[] vertices, uint[] indices, Texture texture)
        {
            Console.WriteLine("Load new sprite...");
            
            LoadResult gpuBindData = GpuLoadData(vertices, indices);

            SpriteShader.Use();
            texture.Use();
            
            int vertexLocation = SpriteShader.GetAttribLocation("aPosition");
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
            
            int texCoordLocation = SpriteShader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            ClearGpuBindedData();

            Console.WriteLine("Load new sprite complete");
            
            return gpuBindData;
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
            Shader.Dispose();
        }

        
        public static void DrawTriangle(int vertexArrayObject, int size, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            Matrix4 modelView = GetModelViewMatrix(postiton, rotation, scale);

            SetShaderData(vertexArrayObject, modelView, Shader);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            
            GL.DrawElements(PrimitiveType.Triangles, size, DrawElementsType.UnsignedInt, 0);
           
            GL.BindVertexArray(0);
        }

        public static void DrawLineLoop(int vertexArrayObject, int size, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            Matrix4 modelView = GetModelViewMatrix(postiton, rotation, scale);
            
            SetShaderData(vertexArrayObject, modelView, Shader);
            
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawElements(PrimitiveType.LineLoop, size, DrawElementsType.UnsignedInt, 0);
           
            GL.BindVertexArray(0);
        }

        public static void DrawPoint(int vertexArrayObject, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            Matrix4 t2 = Matrix4.CreateTranslation(postiton);
            Matrix4 r3 = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(rotation.Z));
            Matrix4 s = Matrix4.CreateScale(1f);
            Matrix4 modelView = s * r3 * t2;
            
            SetShaderData(vertexArrayObject, modelView, Shader);
            
            GL.DrawArrays(PrimitiveType.Points, 0, 1);
            
            GL.BindVertexArray(0);
        }
        
        public static void RenderText(string text, Vector3 position, float scale)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            
            Drawing.DrawingPrimitives.Clear();
            Drawing.Print(Font, text, position, QFontAlignment.Left);           
            Drawing.RefreshBuffers();
            
            Drawing.ProjectionMatrix = ProjectionMatrix;
            Drawing.Draw();
            
            GL.Disable(EnableCap.Blend);
        }
        
        public static void RenderSprite(int vertexArrayObject, int size, Texture texture, Vector3 postiton, Vector3 rotation, Vector3 scale)
        {
            Matrix4 modelView = GetModelViewMatrix(postiton, rotation, scale);

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            
            SetShaderData(vertexArrayObject, modelView, SpriteShader);
            texture.Use();
            
            GL.DrawElements(PrimitiveType.Triangles, size, DrawElementsType.UnsignedInt, 0);
           
            GL.BindVertexArray(0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.Disable(EnableCap.Blend);
        }

        public static void Unload()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);
            
            Shader.Dispose();
            SpriteShader.Dispose();
            Drawing.Dispose();
            Font.Dispose();
        }
    }
}