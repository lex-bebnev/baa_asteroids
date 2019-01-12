using System;
using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

namespace Asteroids.Game.Components.CommonComponents
{
    public class PolygonRenderComponent: BaseComponent
    {
        private float[] _vertices;
        private uint[] _indices;
        private int VAO;
        private int _verticesCount;
        private int VBO;
        private int EBO;
        private int _centerVAO;
        
        public PolygonRenderComponent(int vertexArrayObject, int verticesCount)
        {
            VAO = vertexArrayObject;
            _verticesCount = verticesCount;
        }
        
        //TODO Obsolete constructor?
        public PolygonRenderComponent(float[] vertices, uint[] indices)
        {
            Console.WriteLine("Create new Render component...");
            _vertices = vertices;
            _indices = indices;
            _verticesCount = indices.Length;
            SetupMesh();
            Console.WriteLine("Create new Render component complete.");
        }

        private void SetupMesh()
        {
            Console.WriteLine("Setup polygon mesh for render component...");
            LoadResult buffers = Renderer.LoadObject(_vertices, _indices);
            VAO = buffers.VAO;
            VBO = buffers.VBO;
            EBO = buffers.EBO;
            
            Console.WriteLine("Setup polygon mesh complete.");
        }
        
        public override void Render()
        {
            if (Parent == null)
            {
                Console.WriteLine("Error: Game object is null");
                return;
            }

            Renderer.DrawTriangle(VAO, _verticesCount,
                Parent.TransformComponent.Position,
                Parent.TransformComponent.Rotation,
                Parent.TransformComponent.Scale);
            
            //Renderer.RenderText($"{Parent.TransformComponent.Position.X:F1}, {Parent.TransformComponent.Position.Y:F1}", Parent.TransformComponent.Position, 1);
        }
          
    }
}