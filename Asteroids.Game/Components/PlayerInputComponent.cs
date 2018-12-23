using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.InputHandlers;

namespace Asteroids.Game.Components
{
    public class PlayerInputComponent: IInputComponent
    {
        public void Update(GameObject obj, float elapsedTime)
        {
            ICommand command = obj.StateComponent.InputHandler.HandleInput();
            IStateComponent newState = command?.Execute(obj, elapsedTime);
            if (newState != null) obj.StateComponent = newState;
        }
    }
}