using System.Collections.Generic;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Managers
{
    /// <summary>
    ///     States manager
    /// </summary>
    public static class StateManager
    {
        private static Dictionary<string, IGameState> _gameStateDb = new Dictionary<string, IGameState>();
        private static IGameState _currentGameState = null;

        /// <summary>
        ///     Update current state
        /// </summary>
        /// <param name="elapsedTime">Time since last update</param>
        public static void Update(float elapsedTime)
        {
            _currentGameState?.Update(elapsedTime);
        }

        /// <summary>
        ///     Render current state
        /// </summary>
        public static void Render()
        {
            _currentGameState?.Render();
        }

        /// <summary>
        ///     Add new state
        /// </summary>
        /// <param name="newState"></param>
        public static void AddState(IGameState newState)
        {
            _gameStateDb.Add(newState.Name, newState);
            _currentGameState = newState;
        }
    }
}