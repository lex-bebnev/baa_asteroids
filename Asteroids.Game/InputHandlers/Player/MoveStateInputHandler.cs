using System.Runtime.Serialization.Formatters;
using Asteroids.Engine.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.InputHandlers.Player
{
    public class MoveStateInputHandler: KeyboardInputHandler
    {
        public MoveStateInputHandler(ICommand thrustCommand, 
            ICommand leftRotateCommand, 
            ICommand rightRotateCommand, 
            ICommand fireCommand) : base(thrustCommand, leftRotateCommand, rightRotateCommand, fireCommand)
        {
        }

        public override ICommand HandleInput()
        {
            return base.HandleInput();
        }
    }
}