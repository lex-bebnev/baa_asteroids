using Asteroids.Engine.Common;

namespace Asteroids.Engine.Components.Interfaces
{
    public interface IComponent
    {
        /// <summary>
        ///     Game Object containing a component
        /// </summary>
        GameObject Parent { get; set; }
        
        /// <summary>
        ///     Method implements the component logic
        /// </summary>
        /// <param name="elapsedTime"></param>
        void Update(float elapsedTime);
        
        /// <summary>
        ///     Method implemetns the component render logic
        /// </summary>
        void Render();
    }
}