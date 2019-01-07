using System;
using Asteroids.Engine.Components;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class PlayerStateComponent: BaseComponent
    {
        private bool _isAlive = true;
        private int _score = 0;
        
        public bool IsAlive
        {
            get => _isAlive;
            set => _isAlive = value;
        }
        
        public int Score
        {
            get => _score;
            private set
            {
                if (value < 0) return;
                _score = value;
            } 
        }

        public void IncreaseScore()
        {
            Score++;
            Console.WriteLine($"Score: {Score}");
        }
    }
}