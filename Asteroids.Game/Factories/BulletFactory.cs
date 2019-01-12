using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.PlayerComponents;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

namespace Asteroids.Game.Factories
{
    public static class BulletFactory
    {
        private static readonly float[] Vertices = new float[]
        {
            1.0f, 1.0f, 0.0f,
            -1.0f, 1.0f, 0.0f,
            1.0f, -1.0f, 0.0f,
            -1.0f, -1.0f, 0.0f
        };
        private static readonly uint[] Indices = new uint[]
        {
            0, 1, 2,
            3, 1, 2
        };

        private static LoadResult? GpuBindedData;
        
        public static GameObject GetBulletObject(TransformComponent parentTransformComponent, float baseVelocity, IGameState gameWorld)
        {
            if (!GpuBindedData.HasValue)
                GpuBindedData = Renderer.LoadObject(Vertices, Indices);

            TransformComponent transfom = new TransformComponent(parentTransformComponent.Position, parentTransformComponent.Rotation,
                new Vector3(5.0f, 5.0f, 1.0f),
                parentTransformComponent.Direction);
            
            PhysicsComponent physics = new PhysicsComponent {Velocity = baseVelocity};
            PolygonRenderComponent renderer = new PolygonRenderComponent(GpuBindedData.Value.VAO, Indices.Length);
            SpriteRendererComponent spriteRenderer = new SpriteRendererComponent("bullet-1.png");
            LifetimeComponent bulletLifetime = new LifetimeComponent(gameWorld, 2.0f);
            
            GameObject bullet = new GameObject("Bullet", transfom);
            
            bullet.AddComponent(physics);
            //bullet.AddComponent(renderer);
            bullet.AddComponent(spriteRenderer);
            bullet.AddComponent(bulletLifetime);

            return bullet;
        }

        public static GameObject GetLaserShotObject(TransformComponent parentTransformComponent, float baseVelocity, IGameState gameWorld)
        {
            if (!GpuBindedData.HasValue)
                GpuBindedData = Renderer.LoadObject(Vertices, Indices);

            TransformComponent transfom = new TransformComponent(parentTransformComponent.Position, parentTransformComponent.Direction,
                new Vector3(350.0f, 10.0f, 1.0f),
                parentTransformComponent.Direction);    
            
            PhysicsComponent physics = new PhysicsComponent {Velocity = baseVelocity};
            PolygonRenderComponent renderer = new PolygonRenderComponent(GpuBindedData.Value.VAO, Indices.Length);
            SpriteRendererComponent spriteRenderer = new SpriteRendererComponent("laser-1.png");
            LifetimeComponent bulletLifetime = new LifetimeComponent(gameWorld, 2.0f);
            
            GameObject laserShot = new GameObject("Laser", transfom);
            
            laserShot.AddComponent(physics);
            //laserShot.AddComponent(renderer);
            laserShot.AddComponent(spriteRenderer);
            laserShot.AddComponent(bulletLifetime);

            return laserShot;
        }
    }
}