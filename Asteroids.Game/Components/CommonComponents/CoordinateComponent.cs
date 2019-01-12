using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using OpenTK;

namespace Asteroids.Game.Components.CommonComponents
{
    public class CoordinateComponent: BaseComponent
    {
        private float _borderCoordinateX;
        private float _borderCoordinateY;
        
        public CoordinateComponent(IGameState gameWorld)
        {
            float borderSize = 30.0f; // increase coordinate, for the teleport to occur beyond the edge
            //Because coordinate starts in centre of screen 
            _borderCoordinateX = gameWorld.GameWorldSize[0] / 2.0f + borderSize; //430.0f; //TODO Take from renderer component
            _borderCoordinateY = gameWorld.GameWorldSize[1] / 2.0f + borderSize; //330.0f;
        }

        public override void Update(float elapsedTime)
        {   
            if (Parent.TransformComponent.Position.X > _borderCoordinateX) 
                Parent.TransformComponent.Position = new Vector3(-_borderCoordinateX, -Parent.TransformComponent.Position.Y, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.X < -_borderCoordinateX) 
                Parent.TransformComponent.Position = new Vector3(_borderCoordinateX, -Parent.TransformComponent.Position.Y, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.Y > _borderCoordinateY) 
                Parent.TransformComponent.Position = new Vector3(-Parent.TransformComponent.Position.X, -_borderCoordinateY, Parent.TransformComponent.Position.Z);
            
            if (Parent.TransformComponent.Position.Y < -_borderCoordinateY) 
                Parent.TransformComponent.Position = new Vector3(-Parent.TransformComponent.Position.X, _borderCoordinateY, Parent.TransformComponent.Position.Z);
        }
    }
}