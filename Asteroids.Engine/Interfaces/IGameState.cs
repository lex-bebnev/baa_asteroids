
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
        string Name { get; set; }
        
        /// <summary>
        ///     Is ready game state
        /// </summary>
        bool IsReady { get; set; }
        
        /// <summary>
        ///     Load game state
        /// </summary>
        void Load();
        
        /// <summary>
        ///     Update game state
        /// </summary>
        /// <param name="elapsedTime">Time since last update</param>
        void Update(float elapsedTime);
    }
}