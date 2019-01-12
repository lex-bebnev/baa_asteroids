using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Utils;

namespace Asteroids.Game.Components.CommonComponents
{
    public class SpriteRendererComponent: BaseComponent
    {
        private static readonly float[] _verteces = new float[] {
            0.5f,  0.5f, 0.0f,      1.0f, 1.0f, 
            0.5f, -0.5f, 0.0f,      1.0f, 0.0f, 
            -0.5f, -0.5f, 0.0f,     0.0f, 0.0f,
            -0.5f,  0.5f, 0.0f,     0.0f, 1.0f  };
        private static readonly int _vertexCount = 6;
        private static readonly uint[] _indices = new uint[] { 
            0, 1, 3,
            1, 2, 3 };
        /*
        private static int _gpuVertexBufferHandle;*/
        private Texture _gpuTexture;
        private int VAO;
        private int VBO;
        private int EBO;
        
        
        public SpriteRendererComponent(string spriteName)
        {
            _gpuTexture = new Texture($"D:\\gameProjects\\Asteroids_clone\\Asteroids.Game\\Resources\\Sprites\\{spriteName}");
            LoadResult buffers = Renderer.LoadSprite(_verteces, _indices, _gpuTexture);
            VAO = buffers.VAO;
            VBO = buffers.VBO;
            EBO = buffers.EBO;
        }

        public override void Render()
        {
            Renderer.RenderSprite(VAO, 
                _vertexCount,
                _gpuTexture, 
                Parent.TransformComponent.Position,
                Parent.TransformComponent.Rotation,
                Parent.TransformComponent.Scale);
        }
    }
}