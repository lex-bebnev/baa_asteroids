using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using OpenTK;

namespace Asteroids.Game.Utils
{
    public class ColiderDetector: IColiderDetector
    {
        public bool CheckCollision(GameObject obj1, GameObject obj2)
        {   
            float halfWidthOne = obj1.TransformComponent.Scale.X / 2.0f;
            float halfHeightOne = obj1.TransformComponent.Scale.Y / 2.0f;
            
            float halfWidthTwo = obj2.TransformComponent.Scale.X / 2.0f;
            float halfHeightTwo = obj2.TransformComponent.Scale.Y / 2.0f;
            
            

            Vector3 positionOne = obj1.TransformComponent.Position;
            Vector3 positionTwo = obj2.TransformComponent.Position;
            
            bool collisionX = (positionOne.X + halfWidthOne) >= positionTwo.X - halfWidthTwo &&
                              (positionTwo.X + halfWidthTwo) >= positionOne.X - halfWidthOne;
            bool collisionY = (positionOne.Y + halfHeightOne) >= positionTwo.Y - halfHeightTwo &&
                              (positionTwo.Y + halfHeightTwo) >= positionOne.Y - halfHeightOne;

            return (collisionX && collisionY);
            
        }
    }
}