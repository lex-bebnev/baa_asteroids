using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using OpenTK;

namespace Asteroids.Game.Utils
{
    public class ColiderDetector: IColiderDetector
    {
        /// <summary>
        ///     AABB Check collisions
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public bool CheckCollision(GameObject obj1, GameObject obj2)
        {   
            float halfWidthOne = obj1.TransformComponent.Size.X / 2.0f;
            float halfHeightOne = obj1.TransformComponent.Size.Y / 2.0f;
            
            float halfWidthTwo = obj2.TransformComponent.Size.X / 2.0f;
            float halfHeightTwo = obj2.TransformComponent.Size.Y / 2.0f;
            
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