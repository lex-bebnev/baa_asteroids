using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.EnemyComponents;
using Asteroids.Game.Factories;
using Asteroids.OGL.GameEngine.Managers;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class GunComponent: BaseComponent
    {
        private static float LASER_CHARGE_TIME = 1.5f;
        private static float LASER_COOLDOWN_TIME = 5.0f;
        private static float BASE_BULLET_VELOCITY = 300.0f;
        private static float BASE_LASER_VELOCITY = 400.0f;
        
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
                _laserCooldownTime -= elapsedTime;

            if (InputManager.KeyPress(Key.F))
                RocketFire();

            if (InputManager.KeyDown(Key.Space))
                LaserFire(elapsedTime);

            if (InputManager.KeyRelease(Key.Space))
                _chargeLaserTime = 0;
        }

        private void LaserFire(float elapsedTime)
        {
            if (_laserCooldownTime > 0.0f) return;
            _chargeLaserTime += elapsedTime; //TODO Add FX to the spaceship
            
            if (!(_chargeLaserTime > LASER_CHARGE_TIME)) return;

            GameObject laserShot = BulletFactory.GetLaserShotObject(Parent.TransformComponent, _gameWorld);
            _gameWorld.AddGameObject(laserShot);
            
            Console.WriteLine("Laser Fire!");
            
            _chargeLaserTime = 0;
            _laserCooldownTime = LASER_COOLDOWN_TIME;
        }

        private void RocketFire()
        {
            GameObject bullet = BulletFactory.GetBulletObject(Parent.TransformComponent, BASE_BULLET_VELOCITY, _gameWorld);
            _gameWorld.AddGameObject(bullet);
            
            Console.WriteLine("Rocket Fire!");
        }
    }
}