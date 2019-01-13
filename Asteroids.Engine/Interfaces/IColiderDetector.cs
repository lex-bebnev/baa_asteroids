using Asteroids.Engine.Common;

namespace Asteroids.Engine.Interfaces
{
    public interface IColiderDetector
    {
        bool CheckCollision(GameObject obj1, GameObject obj2);
    }
}