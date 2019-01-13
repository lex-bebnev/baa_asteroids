using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.PlayerComponents;
using Asteroids.Game.Utils;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

namespace Asteroids.Game.Factories
{
    public static class BulletFactory
    {
        private static readonly float[] Vertices = new float[]
        {
            1.0f, -1.0f, 0.0f,
            -1.0f, 0.0f, 0.0f,
            -1.0f, -1.0f, 0.0f,
            1.0f, 0.0f, 0.0f
        };
        private static readonly float[] SpriteVertices = new float[]
        {
            1.0f, -1.0f, 0.0f,   1.0f, 1.0f,
            -1.0f, 0.0f, 0.0f,   1.0f, 0.0f,
            -1.0f, -1.0f, 0.0f,  0.0f, 0.0f,
            1.0f, 0.0f, 0.0f,    0.0f, 1.0f  
        };
        private static readonly uint[] Indices = new uint[]
        {
            0, 1, 2,
            3, 1, 0
        };

        private static LoadResult? GpuBindedData;
        private static readonly float TimeToLive = 2.0f;

        public static GameObject GetBulletObject(TransformComponent parentTransformComponent, float baseVelocity, IGameState gameWorld)
        {
            if (!GpuBindedData.HasValue)
                GpuBindedData = Renderer.LoadObject(Vertices, Indices);

            TransformComponent transfom = new TransformComponent(parentTransformComponent.Position, parentTransformComponent.Rotation,
                new Vector3(5.0f, 5.0f, 1.0f),
                parentTransformComponent.Direction);
            
            PhysicsComponent physics = new PhysicsComponent {Velocity = baseVelocity}; 
            LifetimeComponent bulletLifetime = new LifetimeComponent(gameWorld, TimeToLive);
            
            GameObject bullet = new GameObject("Bullet", transfom);
            
            bullet.AddComponent(physics);

            if(Settings.RenderMode == RenderModes.Polygons)bullet.AddComponent(new PolygonRenderComponent(GpuBindedData.Value.VAO, Indices.Length));
            else bullet.AddComponent(new SpriteRendererComponent("bullet-1.png"));            
            bullet.AddComponent(bulletLifetime);
            
            return bullet;
        }

        public static GameObject GetLaserShotObject(TransformComponent parentTransformComponent, IGameState gameWorld)
        {
            if (!GpuBindedData.HasValue)
                GpuBindedData = Renderer.LoadObject(Vertices, Indices);

            TransformComponent transfom = new TransformComponent(parentTransformComponent.Position, parentTransformComponent.Rotation,
                new Vector3(5.0f, 5.0f, 1.0f),
                parentTransformComponent.Direction);    
            
            PhysicsComponent physics = new PhysicsComponent {Velocity = 0};
            /*
            PolygonRenderComponent renderer = new PolygonRenderComponent(GpuBindedData.Value.VAO, Indices.Length);
            SpriteRendererComponent spriteRenderer = new SpriteRendererComponent("laser-1.png", SpriteVertices, Indices);
            */

            LifetimeComponent lifetime = new LifetimeComponent(gameWorld, TimeToLive);
            LaseBehaviourComponent laserBehaviour = new LaseBehaviourComponent(gameWorld);
            
            GameObject laserShot = new GameObject("Laser", transfom);
            
            laserShot.AddComponent(physics);
            if(Settings.RenderMode == RenderModes.Polygons) laserShot.AddComponent(new PolygonRenderComponent(GpuBindedData.Value.VAO, Indices.Length));
            else laserShot.AddComponent(new SpriteRendererComponent("laser-1.png", SpriteVertices, Indices));
            laserShot.AddComponent(lifetime);
            laserShot.AddComponent(laserBehaviour);

            return laserShot;
        }
    }
}