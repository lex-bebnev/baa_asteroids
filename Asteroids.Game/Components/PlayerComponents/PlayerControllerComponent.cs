using System.Collections.Generic;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Commands.InputHandlers;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class PlayerControllerComponent: BaseComponent
    {
        private PlayerInputHandler _inputHandler; 
        
        public PlayerControllerComponent()
        {
            _inputHandler = new PlayerInputHandler();
        }

        public override void Update(float elapsedTime)
        {
            IEnumerable<ICommand> commands = _inputHandler.HandleInput();

            foreach (ICommand command in commands)
            {
                command.Execute(Parent);
            }
        }
    }
}