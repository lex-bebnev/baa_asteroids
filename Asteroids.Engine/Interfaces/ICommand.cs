using Asteroids.Engine.Common;

namespace Asteroids.Engine.Interfaces
{
    public interface ICommand
    {
        /// <summary>
        ///     Execute command logic
        /// </summary>
        /// <param name="actor"></param>
        void Execute(GameObject actor);
    }
}