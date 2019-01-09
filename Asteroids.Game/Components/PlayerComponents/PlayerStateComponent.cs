using System;
using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

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

        public override void Render()
        {
            Renderer.RenderText($"Score: {Score}", new Vector3(-380, 280, -1.0f), 1.0f);
        }
    }
}