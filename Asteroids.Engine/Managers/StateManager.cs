using System.Collections.Generic;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Managers
{
    /// <summary>
    ///     States manager
    /// </summary>
    public static class StateManager
    {
        private static Dictionary<string, IGameState> GameStatesDictionary = new Dictionary<string, IGameState>();
        private static IGameState CurrentGameState = null;

        /// <summary>
        ///     Update current state
        /// </summary>
        /// <param name="elapsedTime">Time since last update</param>
        public static void Update(float elapsedTime)
        {
            CurrentGameState?.Update(elapsedTime);
        }

        /// <summary>
        ///     Add new state
        /// </summary>
        /// <param name="newState"></param>
        public static void AddState(IGameState newState)
        {
            GameStatesDictionary.Add(newState.Name, newState);
            CurrentGameState = newState;
        }

        /// <summary>
        ///     Get current game state
        /// </summary>
        /// <returns></returns>
        public static IGameState GetCurrentGameState()
        {
            return CurrentGameState;
        }
    }
}