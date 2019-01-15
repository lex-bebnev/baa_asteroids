using System.Collections.Generic;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Commands.ControlCommands;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.Commands.InputHandlers
{
    public class PlayerInputHandler: IInputHandler
    {
        ICommand _upButtonCommand = new VelocityChangeCommand(150.0f);
        ICommand _rightButtonCommand = new RotateCommand(180.0f);
        ICommand _leftButtonCommand = new RotateCommand(-180.0f);
        ICommand _stopCommand = new VelocityChangeCommand();
        ICommand _stopRotateCommand = new RotateCommand();
        
        public IEnumerable<ICommand> HandleInput()
        {
            if (InputManager.KeyDown(Key.W) || InputManager.KeyDown(Key.Up)) yield return _upButtonCommand;
            else if (InputManager.KeyRelease(Key.W) || InputManager.KeyRelease(Key.Up)) yield return _stopCommand;
            if (InputManager.KeyDown(Key.A) || InputManager.KeyDown(Key.Left)) yield return _rightButtonCommand;
            else if (InputManager.KeyDown(Key.D) || InputManager.KeyDown(Key.Right)) yield return _leftButtonCommand;
            else if (InputManager.KeyRelease(Key.A)
                     || InputManager.KeyRelease(Key.Left)
                     || InputManager.KeyRelease(Key.D)
                     || InputManager.KeyRelease(Key.Right)) yield return _stopRotateCommand;
        }
    }
}