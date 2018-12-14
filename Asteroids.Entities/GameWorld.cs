using System.Collections.Generic;

namespace Asteroids.Entities
{
    public class GameWorld
    {
        public List<GameObject> GameObjects { get; }

        public GameWorld()
        {
            GameObjects = new List<GameObject>();
        }
    }
}