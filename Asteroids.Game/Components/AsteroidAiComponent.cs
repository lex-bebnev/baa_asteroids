using System;
using Asteroids.Engine.Components;

namespace Asteroids.Game.Components
{
    public class AsteroidAiComponent: BaseComponent
    {
        private static Random Randomizer = new Random();
        private float _velocity;
        
        public AsteroidAiComponent()
        {
            _velocity = Randomizer.Next(25, 80)/100.0f;
        }

        public override void Update(float elapsedTime)
        {
            PhysicsComponent physics = (PhysicsComponent)Parent.GetComponent<PhysicsComponent>();
            if(physics == null) return;
            physics.Velocity = _velocity;
        }
    }
}