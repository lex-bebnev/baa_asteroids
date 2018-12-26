using System;
using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using OpenTK;

namespace Asteroids.Game.Components
{
    public class UfoAiComponent: BaseComponent
    {
        //private static float VELOCITY = 0.5f;
        
        private IGameState _gameWorld;
        private GameObject _player;
        
        public UfoAiComponent(IGameState gameWorld)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _player = gameWorld.GameObjects.First(item => item.Tag == "Player");
        }

        public override void Update(float elapsedTime)
        {
            var physics = (PhysicsComponent) Parent.GetComponent<PhysicsComponent>();
            if(physics == null) return;
            
            var d = new Vector3(_player.TransformComponent.Position.X, _player.TransformComponent.Position.Y, 0.0f);
            d.Normalize();
            Parent.TransformComponent.Direction = d;
            
            physics.Velocity = 120.0f;
        }
    }
}