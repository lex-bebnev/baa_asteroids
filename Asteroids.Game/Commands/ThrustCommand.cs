using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Common.Player;

namespace Asteroids.Game.Commands
{
    public class ThrustCommand 
    {
        float _velocity;
        private PlayerBaseState _moveState;
        
        public ThrustCommand(float velocity)
        {
            _velocity = velocity;
            _moveState = new PlayerBaseState(0.5f);
        }

        public IStateComponent Execute(GameObject actor, float elapsedTime)
        {
            actor.TransformComponent.Position += actor.TransformComponent.Direction * (_velocity) * elapsedTime;
            return _moveState;
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}