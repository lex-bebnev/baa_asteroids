using System.Collections.Generic;
using Asteroids.Engine.Common;

namespace Asteroids.Engine.Interfaces
{
    /// <summary>
    ///     Game state
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        ///     State name
        /// </summary>
        string Name { get; }
        
        /// <summary>
        ///     Is ready game state
        /// </summary>
        bool IsReady { get; }
        
        /// <summary>
        ///     Load game state
        /// </summary>
        void Load();
        
        /// <summary>
        ///     Update game state
        /// </summary>
        /// <param name="elapsedTime">Time since last update</param>
        void Update(float elapsedTime);

        /// <summary>
        ///     Render game state
        /// </summary>
        void Render();
        
        /// <summary>
        ///     Lists of all game objects contained in
        /// </summary>
        IList<GameObject> GameObjects { get; }
    }
}