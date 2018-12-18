using System.Numerics;

namespace Asteroids.Entities
{
    public class Player : GameObject
    {
        private const int BASE_SCORE = 0;
        
        public int Score { get; set; }
        public bool IsAlive { get; set; }
        
        public Player()
        {
            IsAlive = true;
            Score = BASE_SCORE;
        }
    }
}