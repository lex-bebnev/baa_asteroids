using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Factories;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class BulletCollisionsComponent: BaseComponent
    {
        private IGameState _gameWorld;
        private float _width;
        private float _height;
        private bool _isBreaking;
        
        public BulletCollisionsComponent(IGameState gameWorld, float width, float height, bool isBreaking = false)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _width = width;
            _height = height;

            float x = _width / 2.0f;
            float y = _height / 2.0f;
            _isBreaking = isBreaking;
        }

        public override void Update(float elapsedTime)
        {
            IEnumerable<GameObject> colideObjects = _gameWorld.GameObjects.Where(item => item.Tag == "Bullet").Select(item => item);
            GameObject playerObject = _gameWorld.GameObjects.Where(item => item.Tag == "Player").Select(item => item).SingleOrDefault();
            PlayerStateComponent playerState = (PlayerStateComponent) playerObject?.GetComponent<PlayerStateComponent>();
            
            Vector3 positionTwo = Parent.TransformComponent.Position;
            foreach (GameObject colideObject in colideObjects)
            {
                Vector3 positionOne = colideObject.TransformComponent.Position;
                bool collisionX = (positionOne.X + _width) >= positionTwo.X &&
                                  (positionTwo.X + _width) >= positionOne.X;
                bool collisionY = (positionOne.Y + _height) >= positionTwo.Y &&
                                  (positionTwo.Y + _height) >= positionOne.Y;
                
                if (!(collisionX && collisionY)) continue;
                _gameWorld.GameObjects.Remove(colideObject);
                _gameWorld.GameObjects.Remove(Parent);
                playerState?.IncreaseScore();
                GameObjectDestroy();
                return;
            }
        }

        public void GameObjectDestroy()
        {
            if (Parent.Tag != "Asteroid" || !_isBreaking) return;

            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 25.0f, _gameWorld, false));
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 30.0f, _gameWorld, false));
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 25.0f, _gameWorld, false));
        }
    }
}