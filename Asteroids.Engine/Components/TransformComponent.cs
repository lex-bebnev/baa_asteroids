using OpenTK;

namespace Asteroids.Engine.Components
{
    public class TransformComponent: BaseComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 Direction { get; set; }
        public Vector2 Size { get; set; }
        
        public TransformComponent(Vector3 position, Vector3 rotation, Vector3 scale, Vector3 direction)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Direction = direction;
        }

        public TransformComponent(Vector3 position, Vector3 scale): this(position, Vector3.Zero, scale, Vector3.Zero)
        {
        }

        public TransformComponent(): this(Vector3.Zero, Vector3.Zero, Vector3.One, Vector3.Zero)
        {
        }
    }
}