using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Commands;
using Asteroids.Game.InputHandlers;
using OpenTK;

namespace Asteroids.Game.Common.Player
{
    public class PlayerBaseState: IStateComponent
    {
       public IInputHandler InputHandler { get; }

        public PlayerBaseState()
        {
            InputHandler = new KeyboardInputHandler();
        }

        public void Update(GameObject obj, float elapsedTime)
        {
            InputHandler.HandleInput();
        }
    }
}