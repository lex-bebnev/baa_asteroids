using Asteroids.Engine.Common;

namespace Asteroids.Engine.Components.Interfaces
{
    public interface IGraphicsComponent
    {
        void Update(GameObject obj, ModelComponent model);
    }
}