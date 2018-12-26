using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.Components
{
    public class GunComponent: BaseComponent
    {
        private static float LASER_CHARGE_TIME = 2.0f;
        private static float LASER_COOLDOWN_TIME = 5.0f;
        private static float BASE_BULLET_VELOCITY = 300.0f;
        
        
        private float _chargeLaserTime;
        private float _laserCooldownTime;
        
        private IGameState _gameWorld;

        public GunComponent(IGameState gameWorld)
        {
            _gameWorld = gameWorld ?? throw new ArgumentNullException(nameof(gameWorld));
            _chargeLaserTime = 0;
            _laserCooldownTime = 0;
        }
        
        public override void Update(float elapsedTime)
        {
            HandleFireInput(elapsedTime);
        }
        
        private void HandleFireInput(float elapsedTime)
        {
            if (_laserCooldownTime > 0.0f)
            {
                _laserCooldownTime -= elapsedTime;
            }
            
            if (InputManager.KeyPress(Key.F))
            {
                var transfom = new TransformComponent(Parent.TransformComponent.Position, Parent.TransformComponent.Rotation, new Vector3(5.0f, 5.0f, 1.0f), 
                    Parent.TransformComponent.Direction);
                var bullet = new GameObject("Bullet", transfom);
                
                PhysicsComponent physics = new PhysicsComponent();
                physics.Velocity = BASE_BULLET_VELOCITY;
                bullet.AddComponent(physics);

                var vertices = new float[]
                {
                    0.5f, 0.5f, 0.0f, 
                    -0.5f, 0.5f, 0.0f,
                    0.5f, -0.5f, 0.0f,
                    -0.5f, -0.5f, 0.0f
                };
                var indices = new uint[]
                {
                    0, 1, 2,
                    3, 1, 2
                };
                PolygonRenderComponent renderer = new PolygonRenderComponent(vertices, indices);
                bullet.AddComponent(renderer);

                var bulletLifetime = new LifetimeComponent(_gameWorld, 2.0f);
                bullet.AddComponent(bulletLifetime);

                var colider = new ColisionsComponent(_gameWorld, 1.0f, 1.0f);
                bullet.AddComponent(colider);
                
                _gameWorld.AddGameObject(bullet);
                
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
    }
}