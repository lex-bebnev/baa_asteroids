using Asteroids.Engine.Common;

namespace Asteroids.Engine.Components.Interfaces
{
    public interface IComponent
    {
        GameObject Parent { get; set; }
        void Update(float elapsedTime);
        void Render();
    }
}