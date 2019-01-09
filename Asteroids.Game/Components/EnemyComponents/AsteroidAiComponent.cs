using System;
using Asteroids.Engine.Components;

namespace Asteroids.Game.Components.EnemyComponents
{
    public class AsteroidAiComponent: BaseComponent
    {
        private static Random Randomizer = new Random();
        private float _velocity;
        private float _angularVelocity;
        
        public AsteroidAiComponent(int minVelocity, int maxVelocity)
        {
            _velocity = Randomizer.Next(minVelocity, maxVelocity);
            _angularVelocity = 25.0f;
        }

        public override void Update(float elapsedTime)
        {
            PhysicsComponent physics = (PhysicsComponent)Parent.GetComponent<PhysicsComponent>();
            if(physics == null) return;
            physics.Velocity = _velocity;
            physics.AngularVelocity = _angularVelocity;
        }
    }
}