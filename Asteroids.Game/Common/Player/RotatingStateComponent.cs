using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.Common.Player
{
    public enum RotateDirection
    {
        Left = -1, Right = 1
    }
    
    public class RotatingStateComponent: IStateComponent
    {
        private RotateDirection _rotateDirection;
        private float _rotateVelocity;

        public RotatingStateComponent(RotateDirection rotateDirection, float rotateVelocity)
        {
            _rotateDirection = rotateDirection;
            _rotateVelocity = rotateVelocity;
        }

        public IStateComponent HandleInput()
        {
            if (InputManager.KeyRelease(Key.A))
            {
                return new DragStateComponent(0.0f, 0.0f, 0.0f);
            }
            if (InputManager.KeyRelease(Key.D))
            {
                return new DragStateComponent(0.0f, 0.0f, 0.0f);
            }
            return null;
        }

        public void Update(GameObject obj, float elapsedTime)
        {
            obj.TransformComponent.Rotation += new Vector3(0.0f, 0.0f, (float)_rotateDirection * (_rotateVelocity * elapsedTime));
            float xD = (float) (Math.Sin(obj.TransformComponent.Rotation.Z));//2 * Math.PI * (actor.TransformComponent.Rotation.Z / 360.0f)));
            float yD = (float) (Math.Cos(obj.TransformComponent.Rotation.Z));//2 * Math.PI * (actor.TransformComponent.Rotation.Z / 360.0f)));
            var d = new Vector3(xD, -yD, 0.0f);
            d.Normalize();
            obj.TransformComponent.Direction = d;
        }
    }
}