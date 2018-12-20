using System.Collections.Generic;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Managers
{
    /// <summary>
    ///     States manager
    /// </summary>
    public static class StateManager
    {
        private static Dictionary<string, IGameState> _gameStatesDictionary = new Dictionary<string, IGameState>();
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
        ///     Add new state
        /// </summary>
        /// <param name="newState"></param>
        public static void AddState(IGameState newState)
        {
            _gameStatesDictionary.Add(newState.Name, newState);
            _currentGameState = newState;
        }

        /// <summary>
        ///     Get current game state
        /// </summary>
        /// <returns></returns>
        public static IGameState GetCurrentGameState()
        {
            return _currentGameState;
        }
    }
}