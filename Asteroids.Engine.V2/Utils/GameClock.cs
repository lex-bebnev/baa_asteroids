using System.Diagnostics;

namespace Asteroids.Engine.V2.Utils
{
    /// <summary>
    /// Class of Game Clock
    /// </summary>
    public class GameClock
    {
        private readonly float _timeScale;
        private readonly Stopwatch _stopwatch;
        private bool _isPaused;

        public GameClock(float timeScale = 1.0f)
        {
            _timeScale = timeScale; // default - unscaled
            _isPaused = false; // default to running
            _stopwatch = new Stopwatch();
            _stopwatch.Reset();
        }

        /// <summary>
        /// Pause or continue game clock
        /// </summary>
        public bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }
        
        /// <summary>
        /// Get elapsed time in seconds
        /// </summary>
        /// <returns>Elapsed time</returns>
        public float GetElapsedSeconds()
        {
            float elapsed = (float)_stopwatch.Elapsed.TotalSeconds;
            _stopwatch.Restart();
            return elapsed * _timeScale;
        }
    }
}