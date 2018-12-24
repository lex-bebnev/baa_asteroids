
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;

namespace Asteroids.Engine.Components
{
    public abstract class BaseComponent: IComponent
    {
        public GameObject Parent { get; set; }
        
        /// <summary>
        ///     Update component state
        /// </summary>
        /// <param name="elapsedTime"></param>
        public virtual void Update(float elapsedTime) {}

        /// <summary>
        ///     Render component
        /// </summary>
        public virtual void Render() {}
    }
}