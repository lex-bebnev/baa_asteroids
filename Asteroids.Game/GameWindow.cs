using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.States;
using Asteroids.OGL.GameEngine;

namespace Asteroids.Game
{
    public class GameWindow: BaseEngine
    {
        private readonly Dictionary<string, IGameState> _gameStates = new Dictionary<string, IGameState>();
        private IGameState _currentGameState;
        
        public GameWindow(int width, int height, string title)
            : base(width, height, title) {}
        
        public override void InitializeStates()
        {
            Console.WriteLine("Initialize GameState...");
            
            SandboxGameState level = new SandboxGameState("Level");
            level.Select += OnSelect;
            _gameStates.Add(level.Name, level);
            
            MenuGameState menu = new MenuGameState("Main Menu");
            menu.Select += OnSelect;
            _gameStates.Add(menu.Name, menu);
            
            _currentGameState = menu;
            
            Console.WriteLine("Initialize GameState complete");
        }

        private void OnSelect(string selectedmenu)
        {
            switch (selectedmenu)
            {
                    case "Start":
                       _gameStates.TryGetValue("Level", out _currentGameState);
                        break;
                    case "Exit":
                        Exit();
                        break;
                    case "Menu":
                       _gameStates.TryGetValue("Main Menu", out _currentGameState);
                        break;

            }
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