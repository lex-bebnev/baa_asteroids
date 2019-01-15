using System.Collections.Generic;

namespace Asteroids.Engine.Interfaces
{
    public interface IInputHandler
    {
        IEnumerable<ICommand> HandleInput();
    }
}