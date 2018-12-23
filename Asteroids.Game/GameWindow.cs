using System;
using System.Collections.Generic;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.States;
using Asteroids.OGL.GameEngine;

namespace Asteroids.Game
{
    public class GameWindow: BaseEngine
    {
        private Dictionary<string, IGameState> _gameStates = new Dictionary<string, IGameState>();
        private IGameState _currentGameState;
        
        public GameWindow(int width, int height, string title) : base(width, height, title)
        {
        }
        
        public override void InitializeStates()
        {
            Console.WriteLine("Initialize GameState...");
            var level = new SandboxGameState("Level");
            _gameStates.Add(level.Name, level);
            _currentGameState = level;
            Console.WriteLine("Initialize GameState complete");
        }

        public override void Update(float elapsedMilliseconds)
        {
            if(_currentGameState == null) return;
            if (!_currentGameState.IsReady)
            {
                _currentGameState.Load();
                return;
            }
            _currentGameState.Update(elapsedMilliseconds);
        }

        public override void Render()
        {
            if(_currentGameState == null || !_currentGameState.IsReady) return;
            _currentGameState.Render();
        }
    }
}