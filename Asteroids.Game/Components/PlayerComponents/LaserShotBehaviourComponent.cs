using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using OpenTK;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class LaseBehaviourComponent: BaseComponent
    {
        private static float LASER_VELOCITY = 600.0f;

        private readonly TransformComponent _playerTransformComponent;

        public LaseBehaviourComponent(IGameState gameWorld)
        {
            GameObject player = gameWorld.GameObjects.Where(item => item.Tag == "Player").Select(item => item).SingleOrDefault();
            
            if(player != null)
                _playerTransformComponent = (TransformComponent) player.GetComponent<TransformComponent>();
        }

        public override void Update(float elapsedTime)
        {
            if (_playerTransformComponent == null) return;

            Parent.TransformComponent.Position = _playerTransformComponent.Position;
            Parent.TransformComponent.Rotation = _playerTransformComponent.Rotation;
            Parent.TransformComponent.Direction = _playerTransformComponent.Direction;
            Parent.TransformComponent.Scale +=  new Vector3(0.0f, (LASER_VELOCITY * elapsedTime), 0.0f);
        }
    }
}