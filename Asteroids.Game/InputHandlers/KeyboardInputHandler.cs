using System;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Commands;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.InputHandlers
{
    /// <summary>
    ///     Handle OpenTK input
    /// </summary>
    public class KeyboardInputHandler: IInputHandler
    {
        private ICommand thrustCommand;
        private ICommand leftRotateCommand;
        private ICommand rightRotateCommand;
        private ICommand fireCommand;

        public KeyboardInputHandler()
        {
        }

        public virtual ICommand HandleInput()
        {
            if (InputManager.KeyDown(Key.W))
            {
                return null
            }
            if (InputManager.KeyDown(Key.A))
            {
                return rightRotateCommand; 
            }
            if (InputManager.KeyDown(Key.D))
            {
                return leftRotateCommand; 
            }
            if (InputManager.KeyDown(Key.Space))
            {
                return fireCommand; 
            }

            return null;
        }
    }
}