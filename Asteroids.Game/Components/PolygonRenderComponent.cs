using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

namespace Asteroids.Game.Components
{
    public class PolygonRenderComponent: IGraphicsComponent
    {
        private float[] _vertices;
        private uint[] _indices;
        private int VAO;
        private int VBO;
        private int EBO;
        
        public PolygonRenderComponent(float[] vertices, uint[] indices)
        {
            Console.WriteLine("Create new Render component...");
            _vertices = vertices;
            _indices = indices;
            SetupMesh();
            Console.WriteLine("Create new Render component complete.");
        }

        private void SetupMesh()
        {
            Console.WriteLine("Setup mesh for render component...");
            LoadResult buffers = Renderer.LoadObject(_vertices, _indices);
            VAO = buffers.VAO;
            VBO = buffers.VBO;
            EBO = buffers.EBO;
            
            Console.WriteLine("Setup mesh complete.");
        }
        
        public void Update(GameObject obj)
        {
            if (obj == null)
            {
                Console.WriteLine("Error: Game object is null");
                return;
            }
            
            Renderer.DrawTriangle(VAO, _indices.Length, 
                obj.TransformComponent.Position, 
                obj.TransformComponent.Rotation, 
                obj.TransformComponent.Scale);
        }
    }
}