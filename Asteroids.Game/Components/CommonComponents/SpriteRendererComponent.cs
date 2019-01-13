using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Utils;

namespace Asteroids.Game.Components.CommonComponents
{
    public class SpriteRendererComponent: BaseComponent
    {
        private static readonly float[] Verteces = new float[] {
            0.5f,  0.5f, 0.0f,      1.0f, 1.0f, 
            0.5f, -0.5f, 0.0f,      1.0f, 0.0f, 
            -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,
            -0.5f,  0.5f, 0.0f,     0.0f, 1.0f  };
        private static readonly int VertexCount = 6;
        private static readonly uint[] Indices = new uint[] { 
            0, 1, 3,
            1, 2, 3 };
        
        private Texture _gpuTexture;
        private LoadResult _gpuBindedData;

        public SpriteRendererComponent(string spriteName)
        {
            _gpuTexture = new Texture($@"Resources\\Sprites\\{spriteName}");
            _gpuBindedData = Renderer.LoadSprite(Verteces, Indices, _gpuTexture);
        }
        
        public SpriteRendererComponent(string spriteName, float[] vertices, uint[] indices)
        {
            _gpuTexture = new Texture($@"Resources\\Sprites\\{spriteName}");
            _gpuBindedData = Renderer.LoadSprite(vertices, indices, _gpuTexture);
        }

        public override void Render()
        {
            Renderer.RenderSprite(_gpuBindedData.VAO, 
                VertexCount,
                _gpuTexture, 
                Parent.TransformComponent.Position,
                Parent.TransformComponent.Rotation,
                Parent.TransformComponent.Scale);
        }
    }
}