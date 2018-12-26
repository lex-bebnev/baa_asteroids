using System;
using System.Linq;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.OGL.GameEngine.Utils;

namespace Asteroids.Game.Components
{
    public class ColisionsComponent: BaseComponent
    {
        private IGameState _gameWorld;
        private float _width;
        private float _height;

        private int VAO;
        
        public ColisionsComponent(IGameState gameWorld, float width, float height)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _width = width;
            _height = height;

            float x = _width / 2.0f;
            float y = _height / 2.0f;
            
            VAO = Renderer.LoadObject(new float[]{x,y,0.0f, -x,y,0.0f, x,-y,0.0f, -x,-y,0.0f}, new uint[]{0,1,2, 1,3,2}).VAO;
        }

        public override void Update(float elapsedTime)
        {
            var colideObjects = _gameWorld.GameObjects.Where(item => item.Tag == "Enemy").Select(item => item);
            var positionTwo = Parent.TransformComponent.Position;
            foreach (var colideObject in colideObjects)
            {
                float coliderSize = 20.0f;
                
                var positionOne = colideObject.TransformComponent.Position;
                bool collisionX = (positionOne.X + coliderSize) >= positionTwo.X &&
                                  (positionTwo.X + coliderSize) >= positionOne.X;
                bool collisionY = (positionOne.Y + coliderSize) >= positionTwo.Y &&
                                  (positionTwo.Y + coliderSize) >= positionOne.Y;
                
                if (!(collisionX && collisionY)) continue;
                _gameWorld.GameObjects.Remove(colideObject);
                _gameWorld.GameObjects.Remove(Parent);
                return;
            }
        }

        public override void Render()
        {
            Renderer.DrawTriangle(VAO, 6, Parent.TransformComponent.Position, Parent.TransformComponent.Rotation, Parent.TransformComponent.Scale);
        }
    }
}