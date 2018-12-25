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
            
            float borderCoordinateX = 430.0f; //TODO Take from renderer component
            float borderCoordinateY = 330.0f;
           
            if (Parent.TransformComponent.Position.X > borderCoordinateX) 
                Parent.TransformComponent.Position = new Vector3(-borderCoordinateX, -Parent.TransformComponent.Position.Y, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.X < -borderCoordinateX) 
                Parent.TransformComponent.Position = new Vector3(borderCoordinateX, -Parent.TransformComponent.Position.Y, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.Y > borderCoordinateY) 
                Parent.TransformComponent.Position = new Vector3(-Parent.TransformComponent.Position.X, -borderCoordinateY, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.Y < -borderCoordinateY) 
                Parent.TransformComponent.Position = new Vector3(-Parent.TransformComponent.Position.X, borderCoordinateY, Parent.TransformComponent.Position.Z);
        }
    }
}