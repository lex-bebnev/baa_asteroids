using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Common.Player;
using Asteroids.Game.Components;
using OpenTK;

namespace Asteroids.Game.States
{
    public class SandboxGameState: IGameState
    {
        public string Name { get; }
        public bool IsReady { get; private set; }

        private IList<GameObject> _gameObjects;
        public IList<GameObject> GameObjects
        {
            get { return _gameObjects; }
        }
        
        
        public SandboxGameState(string name)
        {
            Name = name;
            _gameObjects = new List<GameObject>();
            IsReady = false;
        }

        public void Load()
        {
            Console.WriteLine("Load game state...");
            AddPlayer();
            AddUfo();
            AddAsteroid();
            IsReady = true;
            Console.WriteLine("Load gamestate complete");
        }

        private void AddPlayer()
        {
            //TODO remove
            float[] shipVertices =
            {
                //Position          
                0.0f,   0.25f, -1.0f, 
                -0.25f,   0.4f, -1.0f, 
                0.25f,   0.4f, -1.0f, 
                0.0f,  -0.4f, -1.0f
            };
            uint[] shipIndices =
            {
                0, 1, 3,
                0, 3, 2
            };
       
            GameObject player = new GameObject(
                "Player",
                new ColliderPhysicsComponent(),
                new PolygonRenderComponent(shipVertices, shipIndices),
                new TransformComponent(new Vector3(0.5f, 0.0f, 0.0f),
                    new Vector3(0.0f, 0.0f, 0.0f), 
                    new Vector3(0.08f, 0.08f, 1.0f), 
                    new Vector3(0.0f, -1.0f, 0.0f)),
                new DragStateComponent(1.0f, 0.0f, 0.0f));
            
            _gameObjects.Add(player);
        }
        private void AddUfo()
        {
            float[] UfoVertecies =
            {
                -0.5f, 0.0f, 0.0f,
                0.5f, 0.0f, 0.0f,
                -0.25f, -0.2f, 0.0f,
                0.25f, -0.2f, 0.0f,
                -0.25f, 0.2f, 0.0f,
                0.25f, 0.2f, 0.0f,
                -0.05f, 0.5f, 0.0f,
                0.05f, 0.5f, 0.0f
            };
            uint[] UfoIndeces =
            {
                0, 1, 2,
                1, 2, 3,
                0, 4, 1,
                4, 5, 1,
                4, 6, 7,
                4, 5, 7
            };

            var mesh = new PolygonRenderComponent(UfoVertecies, UfoIndeces);
            
            GameObject ufo = new GameObject(
                "Enemy",
                new ColliderPhysicsComponent(),
                mesh,
                new TransformComponent(new Vector3(0.0f, 0.0f, -2.0f),
                    new Vector3(0.0f, 0.0f, -0.5f), 
                    new Vector3(0.2f, 0.2f, 1.0f), 
                    new Vector3(0.0f, 0.0f, 0.0f)),
                null
               );
            
            _gameObjects.Add(ufo);
            
            GameObject ufo2 = new GameObject(
                "Enemy",
                new ColliderPhysicsComponent(),
                mesh,
                new TransformComponent(new Vector3(-0.5f, 0.0f, -2.0f),
                    new Vector3(0.0f, 0.0f, 0.5f), 
                    new Vector3(0.2f, 0.2f, 1.0f), 
                    new Vector3(0.0f, 0.0f, 0.0f)),
                null
            );

            _gameObjects.Add(ufo2);
        }
        private void AddAsteroid()
        {
            Random r = new Random();
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
                var angleVaryRadians = (tau/granularity) * angleVary / 100;
                var angleFinal = angle + angleVaryRadians - (Math.PI / granularity);
                int radius = r.Next(minRadius, maxRadius);

                float x = (float)Math.Sin(angleFinal) * radius;
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
            indices.Add(currentIndex-1);
            indices.Add(1);

            IGraphicsComponent mesh = new PolygonRenderComponent(points.ToArray(), indices.ToArray());
            
            GameObject asteroid = new GameObject(
                "Enemy",
                new ColliderPhysicsComponent(),
                mesh,
                new TransformComponent(new Vector3(0.0f, 0.5f, -2.0f),
                    new Vector3(0.0f, 0.0f, 0.5f), 
                    new Vector3(0.0025f, 0.0025f, 1.0f), 
                    new Vector3(0.0f, 0.0f, 0.0f)),
                null
            );
            
            _gameObjects.Add(asteroid);
        }
        
        public void Update(float elapsedTime)
        {
            if(!IsReady) return;

            foreach (var objects in _gameObjects)
            {
                objects.Update(elapsedTime, this);
            }
        }

        public void Render()
        {
            if(!IsReady) return;

            foreach (var objects in _gameObjects)
            {
                objects.Render();
            }
        }
    }
}