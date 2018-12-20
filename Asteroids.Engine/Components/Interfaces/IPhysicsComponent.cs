using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Components.Interfaces
{
    public interface IPhysicsComponent
    {
        void Update(GameObject obj, IGameState world);
    }
}