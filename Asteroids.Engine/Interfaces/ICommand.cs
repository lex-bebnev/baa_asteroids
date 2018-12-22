using Asteroids.Engine.Common;

namespace Asteroids.Engine.Interfaces
{
    /// <summary>
    ///     Base command interface
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        ///     Execute the command
        /// </summary>
        void Execute(GameObject actor);
        
        /// <summary>
        ///     Undo command
        /// </summary>
        void Undo();
    }
}