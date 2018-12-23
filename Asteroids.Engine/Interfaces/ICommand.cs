using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;

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
        IStateComponent Execute(GameObject actor, float elapsedTime);
        
        /// <summary>
        ///     Undo command
        /// </summary>
        void Undo();
    }
}