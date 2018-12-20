using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.Components
{
    public class PlayerInputComponent: IInputComponent
    {
        public void Update(GameObject obj)
        {
            if (InputManager.KeyDown(Key.W))
            {
                Console.WriteLine("W");
            }
            if (InputManager.KeyDown(Key.A))
            {
                Console.WriteLine("A");
            }
            if (InputManager.KeyDown(Key.D))
            {
                Console.WriteLine("D");
            }
            if (InputManager.KeyDown(Key.Space))
            {
                Console.WriteLine("Space");
            }
        }
    }
}