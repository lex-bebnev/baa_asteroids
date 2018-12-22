using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.InputHandlers;

namespace Asteroids.Game.Components
{
    public class PlayerInputComponent: IInputComponent
    {
        private IInputHandler _inputHandler;

        public PlayerInputComponent(IInputHandler handler)
        {
            _inputHandler = handler;
        }

        public void Update(GameObject obj)
        {
            ICommand command = _inputHandler.HandleInput();
            command?.Execute(obj);
        }
    }
}