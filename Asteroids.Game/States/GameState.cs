using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components;
using Asteroids.Game.InputHandlers;

namespace Asteroids.Game.States
{
    public class GameState: IGameState
    {
        public string Name { get; }
        public bool IsReady { get; private set; }

        private IList<GameObject> _gameObjects;
        public IList<GameObject> GameObjects
        {
            get { return _gameObjects; }
        }
        
        
        public GameState(string name)
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
                new PlayerInputComponent(new KeyboardInputHandler()),
                new ColliderPhysicsComponent(),
                new PolygonRenderComponent(shipVertices, shipIndices));
            player.TransformComponent.Scale.X = 0.08f;
            player.TransformComponent.Scale.Y = 0.08f;
            player.TransformComponent.Scale.Z = 1.0f;
            player.TransformComponent.Position.X = 0.5f;

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
                new AIInputComponent(),
                new ColliderPhysicsComponent(),
                mesh
               );
            ufo.TransformComponent.Scale.X = 0.2f;
            ufo.TransformComponent.Scale.Y = 0.2f;
            ufo.TransformComponent.Scale.Z = 1.0f;
            ufo.TransformComponent.Rotation.Z = 0.5f;
            ufo.TransformComponent.Position.X = 0.0f;
            ufo.TransformComponent.Position.Z = -2.0f;
            
            _gameObjects.Add(ufo);
            
            GameObject ufo2 = new GameObject(
                "Enemy",
                new AIInputComponent(),
                new ColliderPhysicsComponent(),
                mesh
            );
            ufo2.TransformComponent.Scale.X = 0.2f;
            ufo2.TransformComponent.Scale.Y = 0.2f;
            ufo2.TransformComponent.Scale.Z = 1.0f;
            ufo2.TransformComponent.Rotation.Z = 0.5f;
            ufo2.TransformComponent.Position.X = -0.5f;
            ufo2.TransformComponent.Position.Z = -2.0f;

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