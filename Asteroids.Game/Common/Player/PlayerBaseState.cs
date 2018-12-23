using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.Common.Player
{
    public class PlayerBaseState: IStateComponent
    {
        float _velocity;
        private Vector3 _lastDelta;
        
        public PlayerBaseState(float velocity)
        {
            _velocity = velocity;
        }

        public IStateComponent HandleInput()
        {
            if (InputManager.KeyRelease(Key.W))
            {
                Console.WriteLine($"Last delta: {_lastDelta.X} - {_lastDelta.Y}");
                return new DragPlayerState(0.05f, _lastDelta.X, _lastDelta.Y);
            }
            return null;
        }

        public void Update(GameObject obj, float elapsedTime)
        {
            _lastDelta = obj.TransformComponent.Direction * (_velocity) * elapsedTime;
            obj.TransformComponent.Position += _lastDelta;
        }
    }
}