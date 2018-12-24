using System;
using OpenTK;

namespace Asteroids.Engine.Components
{
    public class TransformComponent: BaseComponent //TODO Change to IComponent?
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

        public override void Update(float elapsedTime)
        {
            if(Parent == null) return;
            
            float borderCoordinate = (1.0f + (1.0f / -Parent.TransformComponent.Position.Z))
                                           + (1.0f + (1.0f / -Parent.TransformComponent.Position.Z)) 
                                           * Parent.TransformComponent.Scale.X;

            if (Parent.TransformComponent.Position.X > borderCoordinate) 
                Parent.TransformComponent.Position = new Vector3(-borderCoordinate, -Parent.TransformComponent.Position.Y, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.X < -borderCoordinate) 
                Parent.TransformComponent.Position = new Vector3(borderCoordinate, -Parent.TransformComponent.Position.Y, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.Y > borderCoordinate) 
                Parent.TransformComponent.Position = new Vector3(-Parent.TransformComponent.Position.X, -borderCoordinate, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.Y < -borderCoordinate) 
                Parent.TransformComponent.Position = new Vector3(-Parent.TransformComponent.Position.X, borderCoordinate, Parent.TransformComponent.Position.Z);
        }
    }
}