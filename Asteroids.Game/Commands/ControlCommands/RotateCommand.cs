using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Commands.ControlCommands
{
    public class RotateCommand: ICommand
    {
        private readonly float _angularVelocity;

        public RotateCommand(float angularVelocity = 0)
        {
            _angularVelocity = angularVelocity;
        }

        public void Execute(GameObject actor)
        {
            PhysicsComponent physics = (PhysicsComponent) actor?.GetComponent<PhysicsComponent>();
            if(physics == null) return;
            physics.AngularVelocity = _angularVelocity;
        }
    }
}