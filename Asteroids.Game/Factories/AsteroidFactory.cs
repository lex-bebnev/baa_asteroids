using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.EnemyComponents;
using Asteroids.Game.Components.PlayerComponents;
using OpenTK;

namespace Asteroids.Game.Factories
{
    public static class AsteroidFactory
    {
        private static Random r = new Random();
        
        public static GameObject GetAsteroid(Vector3 coordinate, float scale, IGameState gameWorld, bool isBreaking = true)
        {
            var rendererComponent = GenerateAsteroidMesh();
            var rotations = r.Next(0, 360);
            
            int minVelocity = isBreaking ? 80 : 120;
            int maxVelocity = isBreaking ? 120 : 160;
            
            GameObject asteroid = new GameObject(
                "Asteroid",
                new TransformComponent(coordinate,
                    new Vector3(0.0f, 0.0f, rotations), 
                    new Vector3(scale, scale, 1.0f), 
                    Vector3.Zero)
            );
            asteroid.AddComponent(rendererComponent);
            asteroid.AddComponent(new PhysicsComponent());
            asteroid.AddComponent(new AsteroidAiComponent(minVelocity,maxVelocity));
            asteroid.AddComponent(new BulletCollisionsComponent(gameWorld, 20.0f, 20.0f, isBreaking));
            asteroid.AddComponent(new CoordinateComponent());
            
            return asteroid;
        }
        
        private static IComponent GenerateAsteroidMesh()
        {
            int minRadius = 30;
            int maxRadius = 50;
            int granularity = 21;
            int minVary = 25;
            int maxVary = 75;

            double tau = 2 * Math.PI;

            IList<float> points = new List<float>();
            IList<uint> indices = new List<uint>();

            points.Add(0.0f);
            points.Add(0.0f);
            points.Add(0.0f);

            uint prevIndex = 0;
            uint currentIndex = 1;

            for (float angle = 0; angle < tau; angle += (float) (tau / granularity))
            {
                int angleVary = r.Next(minVary, maxVary);
                var angleVaryRadians = (tau / granularity) * angleVary / 100;
                var angleFinal = angle + angleVaryRadians - (Math.PI / granularity);
                int radius = r.Next(minRadius, maxRadius);

                float x = (float) Math.Sin(angleFinal) * radius;
                float y = (float) -Math.Cos(angleFinal) * radius;

                points.Add(x);
                points.Add(y);
                points.Add(0.0f);

                if (currentIndex == 1)
                {
                    prevIndex = currentIndex;
                    currentIndex++;
                    continue;
                }

                indices.Add(0);
                indices.Add(prevIndex);
                indices.Add(currentIndex);

                prevIndex = currentIndex;
                currentIndex++;
            }

            indices.Add(0);
            indices.Add(currentIndex);
            indices.Add(1);

            IComponent mesh = new PolygonRenderComponent(points.ToArray(), indices.ToArray());
            return mesh;
        }
    }
}