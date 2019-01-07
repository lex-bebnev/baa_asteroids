using Asteroids.Engine.Components;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class PlayerStateComponent: BaseComponent
    {
        private bool _isDead = false;
        private int _score = 0;
        
        public bool IsDead
        {
            get => _isDead;
            set => _isDead = value;
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
        }
    }
}