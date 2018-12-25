using System;
using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.Components
{
    public class ControllerComponent : BaseComponent
    {
        private static float VELOCITY = 0.4f;
        private static float ANGULAR_VELOCITY = 3.0f;
        private static float DGAR_FACTOR = 0.005f;
        private static float LASER_CHARGE_TIME = 2.0f;
        private static float LASER_COOLDOWN_TIME = 5.0f;

        private float _chargeLaserTime = 0;
        private float _laserCooldownTime = 0;
        
        public override void Update(float elapsedTime)
        {
            PhysicsComponent physics = (PhysicsComponent) Parent.GetComponent<PhysicsComponent>(); //TODO Get before update
            if (physics == null) return;
            
            HandleThrustInput(physics);
            HandleRotateInput(physics);
            HandleFireInput(elapsedTime); //TODO Separate component?
        }

        private void HandleFireInput(float elapsedTime)
        {
            if (_laserCooldownTime > 0.0f)
            {
                _laserCooldownTime -= elapsedTime;
            }
            
            if (InputManager.KeyPress(Key.F))
            {
                Console.WriteLine("Rocket Fire!");
            }

            if (InputManager.KeyDown(Key.Space))
            {
                if (_laserCooldownTime > 0.0f) return;
                _chargeLaserTime += elapsedTime;          //TODO Add FX to the spaceship
                if (_chargeLaserTime > LASER_CHARGE_TIME)
                {
                    Console.WriteLine("Laser Fire!");
                    _chargeLaserTime = 0;
                    _laserCooldownTime = LASER_COOLDOWN_TIME;
                }
            }

            if (InputManager.KeyRelease(Key.Space))
            {
                _chargeLaserTime = 0;
            }
        }

        private void HandleThrustInput(PhysicsComponent physics)
        {
            if (InputManager.KeyDown(Key.W))
            {
                physics.Velocity = VELOCITY;
                physics.DragFactor = 1.0f;
            }
            else
            {
                physics.DragFactor = DGAR_FACTOR;
                physics.Velocity = 0;
            }
        }

        private void HandleRotateInput(PhysicsComponent physics)
        {
            if (InputManager.KeyDown(Key.A))
                physics.AngularVelocity = ANGULAR_VELOCITY;
            else if (InputManager.KeyDown(Key.D))
                physics.AngularVelocity = -ANGULAR_VELOCITY;
            else physics.AngularVelocity = 0;
        }
    }
}