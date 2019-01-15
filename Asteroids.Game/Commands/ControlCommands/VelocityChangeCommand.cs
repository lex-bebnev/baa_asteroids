using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Commands.ControlCommands
{
    public class VelocityChangeCommand: ICommand
    {
        private readonly float _velocity;

        public VelocityChangeCommand(float velocity = 0)
        {
            _velocity = velocity;
        }

        public void Execute(GameObject actor)
        {
            PhysicsComponent physics = (PhysicsComponent) actor?.GetComponent<PhysicsComponent>();
            if(physics == null) return;
            physics.Velocity = _velocity;
        }
    }
}