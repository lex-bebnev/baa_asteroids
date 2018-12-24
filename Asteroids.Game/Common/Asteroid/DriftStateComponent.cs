using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using OpenTK;

namespace Asteroids.Game.Common.Asteroid
{
    public class DriftStateComponent: IStateComponent
    {
        private float _velocity;
        
        public DriftStateComponent(float velocity)
        {
            _velocity = velocity;
        }

        public IStateComponent HandleInput()
        {
            return null;
        }

        public void Update(GameObject obj, float elapsedTime)
        {
            float borderCoordinate = 1.3f;
            
            obj.TransformComponent.Position += obj.TransformComponent.Direction * (_velocity) * elapsedTime;
            if (obj.TransformComponent.Position.X > borderCoordinate) 
                obj.TransformComponent.Position = new Vector3(-borderCoordinate, -obj.TransformComponent.Position.Y, obj.TransformComponent.Position.Z);
            
            if (obj.TransformComponent.Position.X < -borderCoordinate) 
                obj.TransformComponent.Position = new Vector3(borderCoordinate, -obj.TransformComponent.Position.Y, obj.TransformComponent.Position.Z);
            
            if (obj.TransformComponent.Position.Y > borderCoordinate) 
                obj.TransformComponent.Position = new Vector3(-obj.TransformComponent.Position.X, -borderCoordinate, obj.TransformComponent.Position.Z);
            
            if (obj.TransformComponent.Position.Y < -borderCoordinate) 
                obj.TransformComponent.Position = new Vector3(-obj.TransformComponent.Position.X, borderCoordinate, obj.TransformComponent.Position.Z);
        }
    }
}