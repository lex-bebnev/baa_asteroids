using Asteroids.Engine.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.InputHandlers.Player
{
    public class DragStateInputHandler: KeyboardInputHandler
    {
        private ICommand _stopCommand;
        
        public DragStateInputHandler(ICommand thrustCommand, 
            ICommand leftRotateCommand, 
            ICommand rightRotateCommand, 
            ICommand fireCommand, ICommand stopCommand) : base(thrustCommand, leftRotateCommand, rightRotateCommand, fireCommand)
        {
            _stopCommand = stopCommand;
        }

        public override ICommand HandleInput()
        {
            if (InputManager.KeyRelease(Key.W))
            {
                return _stopCommand;
            }
            return base.HandleInput();
        }
    }
}