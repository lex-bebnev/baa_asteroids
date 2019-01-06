using System;
using System.Linq;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Factories;
using Asteroids.OGL.GameEngine.Utils;

namespace Asteroids.Game.Components
{
    public class BulletCollisionsComponent: BaseComponent
    {
        private IGameState _gameWorld;
        private float _width;
        private float _height;
        private bool _isBreaking;

        private int VAO;
        
        public BulletCollisionsComponent(IGameState gameWorld, float width, float height, bool isBreaking = false)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _width = width;
            _height = height;

            float x = _width / 2.0f;
            float y = _height / 2.0f;
            _isBreaking = isBreaking;
            
            VAO = Renderer.LoadObject(new float[]{x,y,0.0f, -x,y,0.0f, x,-y,0.0f, -x,-y,0.0f}, new uint[]{0,1,2, 1,3,2}).VAO;
        }

        public override void Update(float elapsedTime)
        {
            var colideObjects = _gameWorld.GameObjects.Where(item => item.Tag == "Bullet").Select(item => item);
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
                _gameWorld.GameObjects.Remove(colideObject);
                GameObjectDestroy();
                _gameWorld.GameObjects.Remove(Parent);
                return;
            }
        }


        public void GameObjectDestroy()
        {
            if (Parent.Tag != "Asteroid" || !_isBreaking) return;

            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 0.3f, _gameWorld, false));
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 0.4f,_gameWorld, false));
            _gameWorld.AddGameObject(AsteroidFactory.GetAsteroid(Parent.TransformComponent.Position, 0.3f,_gameWorld, false));
        }
        
        public override void Render()
        {
            //Renderer.DrawTriangle(VAO, 6, Parent.TransformComponent.Position, Parent.TransformComponent.Rotation, Parent.TransformComponent.Scale);
        }
    }
}