using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using OpenTK;

namespace Asteroids.Engine.Components
{
    public class TransformComponent: ITransformComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 Direction { get; set; }
        
        public TransformComponent(Vector3 position, Vector3 rotation, Vector3 scale, Vector3 direction)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Direction = direction;
        }

        public void Update(GameObject obj)
        {
        }
    }
}