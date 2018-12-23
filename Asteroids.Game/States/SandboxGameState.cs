using System;
using System.Collections.Generic;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
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