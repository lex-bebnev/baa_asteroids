using Asteroids.Engine.Components;

namespace Asteroids.Game.Components.CommonComponents
{
    public class SpriteRendererComponent: BaseComponent
    {
        private static readonly float[] _verteces = new float[] {
            1f, 1f, 0f,   0f, 1f, 0f,   1f, 0f, 0f,
            0f, 0f, 0f,   1f, 0f, 0f,   0f, 1f, 0f };
        private static readonly int _vertexCount = 6;
        private static readonly float[] _textureMappingsDefault = new float[] {
            1f,0f,  0f,0f,  1f, 1f,
            0f,1f,  1f,1f,  0f, 0f }; 
        //private Texture _texture;
        private static int _gpuVertexBufferHandle;
        private static int _gpuTextureMappingBufferHandleDefault;
        private int _gpuTextureMappingBufferHandle;
        
    }
}