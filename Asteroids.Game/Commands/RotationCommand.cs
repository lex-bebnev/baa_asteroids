using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Common.Player;
using OpenTK;

namespace Asteroids.Game.Commands
{
    public enum RotateDirection
    {
        Left = -1, Right = 1
    }
    
    public class RotationCommand
    {
        private RotateDirection _rotateDirection;
        float _rotateVelocity;
        
        public RotationCommand(RotateDirection rotateDirection, float rotateVelocity)
        {
            _rotateDirection = rotateDirection;
            _rotateVelocity = rotateVelocity;
        }

        public IStateComponent Execute(GameObject actor, float elapsedTime)
        {
            actor.TransformComponent.Rotation += new Vector3(0.0f, 0.0f, (float)_rotateDirection * (_rotateVelocity * elapsedTime));
            float xD = (float) (Math.Sin(actor.TransformComponent.Rotation.Z));//2 * Math.PI * (actor.TransformComponent.Rotation.Z / 360.0f)));
            float yD = (float) (Math.Cos(actor.TransformComponent.Rotation.Z));//2 * Math.PI * (actor.TransformComponent.Rotation.Z / 360.0f)));
            var d = new Vector3(xD, -yD, 0.0f);
            d.Normalize();
            actor.TransformComponent.Direction = d;
            return null;
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}