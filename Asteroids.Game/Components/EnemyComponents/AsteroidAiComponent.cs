using System;
using Asteroids.Engine.Components;

namespace Asteroids.Game.Components.EnemyComponents
{
    public class AsteroidAiComponent: BaseComponent
    {
        private static Random Randomizer = new Random();
        private float _velocity;
        private float _angularVelocity;
        private PhysicsComponent _physics;
        
        
        public AsteroidAiComponent(int minVelocity, int maxVelocity)
        {
            _velocity = Randomizer.Next(minVelocity, maxVelocity);
            _angularVelocity = 25.0f;
        }

        public override void Update(float elapsedTime)
        {
            if (_physics == null)
            {
                _physics = (PhysicsComponent)Parent.GetComponent<PhysicsComponent>();
                if (_physics == null) return;
            }
            
            _physics.Velocity = _velocity;
            _physics.AngularVelocity = _angularVelocity;
        }
    }
}