using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Components.Interfaces
{ 
    public interface IStateComponent
    {
        IInputHandler InputHandler { get; }
        
        void Update(GameObject obj, float elapsedTime);
    }
}