using System;
using Asteroids.Engine.Common;

namespace Asteroids.Engine.Components
{
    public class TransformComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public TransformComponent(Vector3 position = new Vector3(), Vector3 rotation = new Vector3(), Vector3 scale = new Vector3())
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
    }
}