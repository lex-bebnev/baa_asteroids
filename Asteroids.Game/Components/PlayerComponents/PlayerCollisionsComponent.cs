using System;
using System.Linq;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class PlayerCollisionsComponent: BaseComponent
    {
        private IGameState _gameWorld;
        private float _width;
        private float _height;

        public PlayerCollisionsComponent(IGameState gameWorld, float width, float height)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _width = width;
            _height = height;
        }

        public override void Update(float elapsedTime)
        {
            var colideObjects = _gameWorld.GameObjects.Where(item => item.Tag == "Asteroid" || item.Tag == "Ufo").Select(item => item);
            var positionTwo = Parent.TransformComponent.Position;
            foreach (var colideObject in colideObjects)
            {
                float coliderSize = 20.0f; //TODO Unnecessary
                
                var positionOne = colideObject.TransformComponent.Position;
                bool collisionX = (positionOne.X + _width/*coliderSize*/) >= positionTwo.X &&
                                  (positionTwo.X + _width/*coliderSize*/) >= positionOne.X;
                bool collisionY = (positionOne.Y + _height/*coliderSize*/) >= positionTwo.Y &&
                                  (positionTwo.Y + _height/*coliderSize*/) >= positionOne.Y;
                
                if (!(collisionX && collisionY)) continue;
                //_gameWorld.GameObjects.Remove(Parent);
                var playerState = (PlayerStateComponent)Parent.GetComponent<PlayerStateComponent>();
                if(playerState != null)
                    playerState.IsAlive = false;
                return;
            }
        }
    }
}