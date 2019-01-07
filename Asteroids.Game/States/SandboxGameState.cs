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
using Asteroids.Game.Factories;
using Asteroids.OGL.GameEngine.Utils;
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

        private Random r;
        
        public void Load()
        {
            Console.WriteLine("Load game state...");
            
            AddPlayer();
            
            AddGameObject(UfoFactory.GetUfoGameObject(new Vector3(-300.0f, -150.0f, -2.0f), new Vector3(45.0f, 35.0f, 1.0f), this));
            AddGameObject(UfoFactory.GetUfoGameObject(new Vector3(300.0f, 150.0f, -2.0f), new Vector3(45.0f, 35.0f, 1.0f), this));
           
            AddGameObject(AsteroidFactory.GetAsteroid(new Vector3(50.0f, 0.0f, -2.0f), 1.0f, this));
            AddGameObject(AsteroidFactory.GetAsteroid(new Vector3(400.0f, -200.0f, -2.0f), 1.0f, this));
            AddGameObject(AsteroidFactory.GetAsteroid(new Vector3(-350.0f, -230.0f, -2.0f), 1.0f, this));
            
            IsReady = true;
            Console.WriteLine("Load gamestate complete");
        }

        public void AddGameObject(GameObject obj)
        {
            if(obj == null) return;
            GameObjects.Add(obj);
        }
        
        public void Update(float elapsedTime)
        {
            if(!IsReady) return;

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Update(elapsedTime, this);
            }
        }

        public void Render()
        {
            if(!IsReady) return;

            for (int i = 0; i < _gameObjects.Count; i++)
            {
                _gameObjects[i].Render();
            }
        }


        #region Private methods

        private void AddPlayer()
        {
            //TODO Create resource manager
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
                new TransformComponent(new Vector3(0.5f, 0.0f, -2.0f),
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(45.0f, 45.0f, 1.0f),
                    new Vector3(0.0f, -1.0f, 0.0f)));
            
            player.AddComponent(new PolygonRenderComponent(shipVertices, shipIndices));     
            player.AddComponent(new ControllerComponent());     
            player.AddComponent(new PhysicsComponent());
            player.AddComponent(new GunComponent(this));
            player.AddComponent(new CoordinateComponent());
            player.AddComponent(new PlayerCollisionsComponent(this, 20.0f, 20.0f));
            player.AddComponent(new PlayerStateComponent());
            
            AddGameObject(player);
        }
        
            
        #endregion

    }
}