using System;
using System.Diagnostics;

namespace Asteroids.Engine.Utils
{
    public class GameClock
    {
        private float _timeScale;
        private bool _isPaused;
        private Stopwatch _stopwatch;
        
        
        public GameClock()
        {
            _timeScale = 1.0f; // default - unscaled
            _isPaused = false; // default to running
            _stopwatch = new Stopwatch();
            _stopwatch.Reset();
        }

        public bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }
        
        public float GetElaspedMilliseconds()
        {
            float elapsed = (float)_stopwatch.Elapsed.TotalMilliseconds;
            _stopwatch.Restart();
            return elapsed * _timeScale;
        }
    }
}