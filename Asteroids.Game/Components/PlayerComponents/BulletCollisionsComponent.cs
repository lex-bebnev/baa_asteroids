using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Factories;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class BulletCollisionsComponent: BaseComponent
    {
        private IGameState _gameWorld;
        private bool _isBreaking;
        //private ColiderComponent _coliderComponent;
        
        public BulletCollisionsComponent(IGameState gameWorld, bool isBreaking = false)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _isBreaking = isBreaking;
        }

        public override void Update(float elapsedTime)
        {
            //if (_coliderComponent == null)
            {
                //_coliderComponent = (ColiderComponent) Parent.GetComponent<ColiderComponent>();
                //if (_coliderComponent == null) return;
            }
            
            IEnumerable<GameObject> colideObjects = _gameWorld.GameObjects.Where(item => item.Tag == "Bullet").Select(item => item);
            GameObject playerObject = _gameWorld.GameObjects.Where(item => item.Tag == "Player").Select(item => item).SingleOrDefault();
            PlayerStateComponent playerState = (PlayerStateComponent) playerObject?.GetComponent<PlayerStateComponent>();

            foreach (GameObject colideObject in colideObjects)
            {
                //ColiderComponent objectsColiderComponent = (ColiderComponent) colideObject.GetComponent<ColiderComponent>();
                //if(objectsColiderComponent == null) continue;
                
               // bool isColide = _coliderComponent.CheckCollision(colideObject.TransformComponent.Position,objectsColiderComponent);
                    
                //if(!isColide) continue;
                
                _gameWorld.GameObjects.Remove(colideObject);
                _gameWorld.GameObjects.Remove(Parent);
                playerState?.IncreaseScore();
                GameObjectDestroy(colideObject.Tag);
                return;
            }
        }

        public void GameObjectDestroy(string colideObjectTag)
        {
            if (Parent.Tag != "Asteroid" || !_isBreaking) return;
        
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 25.0f, _gameWorld, false));
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 30.0f, _gameWorld, false));
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 25.0f, _gameWorld, false));
        }
    }
}