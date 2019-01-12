using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using OpenTK;

namespace Asteroids.Game.Components.EnemyComponents
{
    public class UfoAiComponent: BaseComponent
    {
        private static float VELOCITY = 80.0f;
        
        private GameObject _player;
        private PhysicsComponent _physicsComponent;
        
        public UfoAiComponent(IGameState gameWorld)
        {
            _player = gameWorld.GameObjects.First(item => item.Tag == "Player");
        }

        public override void Update(float elapsedTime)
        {
            if(_player == null) return;
            if (_physicsComponent == null)
            {
                 _physicsComponent = (PhysicsComponent) Parent.GetComponent<PhysicsComponent>();
                if (_physicsComponent == null) return;
            }
            
            Vector3 direction = _player.TransformComponent.Position - Parent.TransformComponent.Position;
            direction.Normalize();
            Parent.TransformComponent.Direction = direction;
            
            _physicsComponent.Velocity = VELOCITY; //TODO increase velocity with distance
        }
    }
}