using System;
using System.Collections.Generic;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.PlayerComponents;
using Asteroids.Game.Factories;
using Asteroids.Game.Utils;
using Asteroids.OGL.GameEngine.Managers;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.States
{
    public class SandboxGameState: IGameState
    {
        public string Name { get; }
        public bool IsReady { get; private set; }
        public float[] GameWorldSize { get; } = { 800.0f, 600.0f}; //Width and height 2D world

        private IColiderDetector _coliderDetector;
        
        private IList<GameObject> _gameObjects;
        public IList<GameObject> GameObjects
        {
            get { return _gameObjects; }
        }

        private GameObject _player;
        private PlayerStateComponent _playerState;

        private int _ufoCount = 0;
        private int _asteroidsCount = 0;

        private Random _randomizer = new Random();
        
        public delegate void SelectHandler(string selectedMenu);
        public event SelectHandler Select; 
        
        public SandboxGameState(string name)
        {
            Name = name;
            _gameObjects = new List<GameObject>();
            _coliderDetector = new ColiderDetector();
            IsReady = false;
        }
        
        public void Load()
        {
            Console.WriteLine("Load game state...");
            
            _ufoCount = 0;
            _asteroidsCount = 0;
            
            _player = AddPlayer();
            
            AddGameObject(_player);
            
            SpawnEnemies();

            IsReady = true;
            Console.WriteLine("Load gamestate complete");
        }

        private void SpawnEnemies()
        {
            SpawnUfo(1);
            SpawnAsteroids(4);
        }

        private void SpawnUfo(int count)
        {
            for (int i = 0; i < count; i++)
            {
                AddGameObject(UfoFactory.GetUfoGameObject(new Vector3(_randomizer.Next(-390, 390), -300.0f, -2.0f), new Vector3(45.0f, 25.0f, 1.0f),
                    this));
                _ufoCount++;
            }
        }
        
        private void SpawnAsteroids(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int value = _randomizer.Next(0, 100);
                int y = value > 50 ? -300 : 300; 
                
                AddGameObject(AsteroidFactory.GetAsteroid(new Vector3(_randomizer.Next(-390, 390), y, -2.0f), 50.0f, this));
                _asteroidsCount++;
            }
        }
        
        private void SpawnAsteroidsFragments(int count, Vector3 coordinate)
        {
            for (int i = 0; i < count; i++)
            {
                int value = _randomizer.Next(0, 100);
                int y = value > 50 ? -320 : 320; 
                
                AddGameObject(AsteroidFactory.GetAsteroid(coordinate, 25.0f, this, false));
            }
        }

        public void AddGameObject(GameObject obj)
        {
            if(obj == null) return;
            GameObjects.Add(obj);
        }

        public void RemoveGameObject(GameObject obj)
        {
            GameObjects.Remove(obj);
            switch (obj.Tag)
            {
                case "Asteroid":
                {
                    _asteroidsCount--;
                    break;
                }
                case "Ufo":
                {
                    _ufoCount--;
                    break;
                }
                default: break;
            }
        }

        public void Update(float elapsedTime)
        {
            if(!IsReady) return;

            if (!_playerState.IsAlive)
            {
                if (InputManager.KeyDown(Key.Enter))
                {
                    _gameObjects = new List<GameObject>();
                    Load();
                }
                else
                if (InputManager.KeyPress(Key.Escape))
                {
                    IsReady = false;
                    _gameObjects.Clear();
                    Select?.Invoke("Menu");
                }

                return;
            }
            
            GameObjectsUpdate(elapsedTime);
            CheckCollisions();
            GameLogicUpdate();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                for (int j = i+1; j < GameObjects.Count; j++)
                {
                    bool isColide = _coliderDetector.CheckCollision(GameObjects[i],GameObjects[j]);
                    if(!isColide) continue;
                    ResolveCollision(GameObjects[i], GameObjects[j]);
                }
            }
        }

        private void ResolveCollision(GameObject obj1, GameObject obj2)
        {
            if (obj1.Tag == "Player" && IsEnemy(obj2))
            {
                _playerState.IsAlive = false;
                return;
            }
            if (IsBulletEnemyCollision(obj1, obj2))
            {
                RemoveGameObject(obj1);
                RemoveGameObject(obj2);
                _playerState.IncreaseScore();
                if(IsBulletAsteroidCollision(obj1, obj2))
                    SpawnAsteroidsFragments(3, GetAsteroidCoordinate(obj1, obj2));   
            }
        }

        private static Vector3 GetAsteroidCoordinate(GameObject obj1, GameObject obj2)
        {
            return obj1.Tag == "Asteroid" ? obj1.TransformComponent.Position : obj2.TransformComponent.Position;
        }

        private static bool IsBulletAsteroidCollision(GameObject obj1, GameObject obj2)
        {
            return (obj1.Tag == "Bullet" && obj2.Tag == "Asteroid") 
                   || (obj1.Tag == "Asteroid" && obj2.Tag == "Bullet");
        }
        
        private static bool IsBulletEnemyCollision(GameObject obj1, GameObject obj2)
        {
            return (obj1.Tag == "Bullet" && IsEnemy(obj2)) 
                   || (IsEnemy(obj1) && obj2.Tag == "Bullet");
        }

        private static bool IsEnemy(GameObject obj)
        {
            return (obj.Tag == "Ufo" || obj.Tag == "Asteroid" || obj.Tag == "Fragment");
        }

        private void GameObjectsUpdate(float elapsedTime)
        {
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

            if (_playerState.IsAlive) return;
            Renderer.RenderText("GAME OVER", new Vector3(-45.0f, 50.0f, -1.0f), 1);
            Renderer.RenderText("Press \"ENTER\" to restart", new Vector3(-100.0f, -150.0f, -1.0f), 1);
        }

        private void GameLogicUpdate()
        {
            if (_ufoCount < 1) SpawnUfo(1);
            if (_asteroidsCount < 4) SpawnAsteroids(1);
        }
        
        #region Private methods

        private GameObject AddPlayer()
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
                new TransformComponent(new Vector3(0.0f, 0.0f, -2.0f),
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(45.0f, 45.0f, 1.0f),
                    new Vector3(0.0f, 0.0f, 0.0f)));
            
            if(Settings.RenderMode == RenderModes.Polygons) player.AddComponent(new PolygonRenderComponent(shipVertices, shipIndices));     
            else player.AddComponent(new SpriteRendererComponent("ship-1.png"));
            
            player.AddComponent(new ControllerComponent());
            player.AddComponent(new PhysicsComponent());
            player.AddComponent(new GunComponent(this));
            player.AddComponent(new CoordinateComponent(this));
            PlayerStateComponent state = new PlayerStateComponent();
            player.AddComponent(state);

            _playerState = state;
            
            return player;
        }
            
        #endregion

    }
}