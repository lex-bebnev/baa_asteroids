namespace Asteroids.Engine.Components
{
    /// <summary>
    ///     Model for game objects(vertecies)
    /// </summary>
    public class ModelComponent
    {
        /// <summary>
        ///     Verticies of object
        /// </summary>
        public float[] Verticies { get; }
        
        /// <summary>
        ///     Indicies of vertex
        /// </summary>
        public uint[] Indices { get;  }

        public ModelComponent(float[] verticies, uint[] indices)
        {
            Verticies = verticies;
            Indices = indices;
        }
    }
}