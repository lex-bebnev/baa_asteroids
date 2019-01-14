using System;
using System.Collections.Generic;
using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;
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
        private static float UFO_RESPAWN_TIME = 1.0f;

        private readonly IColiderDetector _coliderDetector;
        private GameObject _player;
        private PlayerStateComponent _playerState;
        private int _ufoCount;
        private int _asteroidsCount;
        private float _ufoDeadTime;
        private readonly Random _randomizer = new Random();
        
        public delegate void SelectHandler(string selectedMenu);
        public event SelectHandler Select; 
        public IList<GameObject> GameObjects { get; private set; }

        public string Name { get; }
        public bool IsReady { get; private set; }
        public float[] GameWorldSize { get; } = { 800.0f, 600.0f}; //Width and height 2D world
        
        public SandboxGameState(string name)
        {
            Name = name;
            GameObjects = new List<GameObject>();
            _coliderDetector = new ColiderDetector();
            IsReady = false;
        }
        
        public void Load()
        {
            Console.WriteLine("Load game state...");
            
            _ufoCount = 0;
            _asteroidsCount = 0;
            
            _player = PlayerFatory.GetPlayer(this);
            _playerState = (PlayerStateComponent) _player.GetComponent<PlayerStateComponent>();
            
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
                AddGameObject(UfoFactory.GetUfoGameObject(new Vector3(_randomizer.Next(-390, 390), 310.0f, -2.0f), 
                    new Vector3(45.0f, 25.0f, 1.0f),
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
                    _asteroidsCount--;
                    break;
                case "Ufo":
                    _ufoCount--;
                    break;
            }
        }

        public void Update(float elapsedTime)
        {
            if(!IsReady) return;

            if (InputManager.KeyPress(Key.Escape))
            {
                IsReady = false;
                GameObjects.Clear();
                Select?.Invoke("Menu"); 
            }
            
            if (!_playerState.IsAlive)
            {
                if (InputManager.KeyDown(Key.Enter))
                {
                    GameObjects = new List<GameObject>();
                    Load();
                }
                return;
            }
            
            GameObjectsUpdate(elapsedTime);
            CheckCollisions();
            GameLogicUpdate(elapsedTime);
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
                return;
            }
            if (IsLaserEnemyCollision(obj1, obj2))
            {
                RemoveGameObject(obj1.Tag == "Laser" ? obj2 : obj1);
                _playerState.IncreaseScore();
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

        private static bool IsLaserEnemyCollision(GameObject obj1, GameObject obj2)
        {
            return (obj1.Tag == "Laser" && IsEnemy(obj2)) 
                   || (IsEnemy(obj1) && obj2.Tag == "Laser");
        }
        
        private static bool IsEnemy(GameObject obj)
        {
            return (obj.Tag == "Ufo" || obj.Tag == "Asteroid" || obj.Tag == "Fragment");
        }

        private void GameObjectsUpdate(float elapsedTime)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update(elapsedTime, this);
            }
        }

        public void Render()
        {
            if(!IsReady) return;

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Render();
            }

            if (_playerState.IsAlive) return;
            Renderer.RenderText("GAME OVER", new Vector3(-45.0f, 50.0f, -1.0f), 1);
            Renderer.RenderText("Press \"ENTER\" to restart", new Vector3(-100.0f, -150.0f, -1.0f), 1);
            Renderer.RenderText("Press \"ESC\" and back in Menu", new Vector3(-115.0f, -175.0f, -1.0f), 1);
        }

        private void GameLogicUpdate(float elapsedTime)
        {
            if (_ufoCount < 1)
            {
                _ufoDeadTime += elapsedTime;
                if (_ufoDeadTime > UFO_RESPAWN_TIME)
                {
                    SpawnUfo(1);
                    _ufoDeadTime = 0;
                }
            }
            if (_asteroidsCount < 4) SpawnAsteroids(1);
        }
    }
}