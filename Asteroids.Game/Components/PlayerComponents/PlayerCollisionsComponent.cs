using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using OpenTK;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class PlayerCollisionsComponent: BaseComponent
    {
        private readonly IGameState _gameWorld;
        private readonly float _width;
        private readonly float _height;

        public PlayerCollisionsComponent(IGameState gameWorld, float width, float height)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _width = width;
            _height = height;
        }

        public override void Update(float elapsedTime)
        {
            IEnumerable<GameObject> colideObjects = _gameWorld.GameObjects.Where(item => item.Tag == "Asteroid" || item.Tag == "Ufo").Select(item => item);
            Vector3 positionTwo = Parent.TransformComponent.Position;
            foreach (GameObject colideObject in colideObjects)
            {
                Vector3 positionOne = colideObject.TransformComponent.Position;
                bool collisionX = (positionOne.X + _width) >= positionTwo.X &&
                                  (positionTwo.X + _width) >= positionOne.X;
                bool collisionY = (positionOne.Y + _height) >= positionTwo.Y &&
                                  (positionTwo.Y + _height) >= positionOne.Y;
                
                if (!(collisionX && collisionY)) continue;
                
                PlayerStateComponent playerState = (PlayerStateComponent)Parent.GetComponent<PlayerStateComponent>();
                if(playerState != null)
                    playerState.IsAlive = false;
                return;
            }
        }
    }
}