using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Components.Interfaces
{ 
    public interface IStateComponent
    {
        IStateComponent HandleInput();
        
        void Update(GameObject obj, float elapsedTime);
    }
}