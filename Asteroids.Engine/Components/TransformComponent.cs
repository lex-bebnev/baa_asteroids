using System;
using Asteroids.Engine.Common;

namespace Asteroids.Engine.Components
{
    public class TransformComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public TransformComponent(Vector3 position = null, Vector3 rotation = null, Vector3 scale = null)
        {
            Position = position ?? new Vector3();
            Rotation = rotation ?? new Vector3();
            Scale = scale ?? new Vector3();
        }
    }
}