
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;

namespace Asteroids.Engine.Components
{
    public abstract class BaseComponent: IComponent
    {
        public GameObject Parent { get; set; }
        
        public virtual void Update(float elapsedTime) {}

        public virtual void Render() {}
    }
}