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
        public ICommand HandleInput()
        {
            if (InputManager.KeyDown(Key.W))
            {   
                return new DebugCommand("W");
            }
            if (InputManager.KeyDown(Key.A))
            {   
                return new DebugCommand("A");
            }
            if (InputManager.KeyDown(Key.D))
            {   
                return new DebugCommand("D");
            }
            if (InputManager.KeyDown(Key.Space))
            {
                return new DebugCommand("Space");
            }

            return null;
        }
    }
}