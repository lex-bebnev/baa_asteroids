using Asteroids.Engine.Components;
using OpenTK;

namespace Asteroids.Game.Components.CommonComponents
{
    public class CoordinateComponent: BaseComponent
    {
        public override void Update(float elapsedTime)
        {
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