using System.Collections.Generic;
using System.Linq;
using Asteroids.Engine.Components;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Engine.Common
{
    /// <summary>
    ///     Base game object class
    /// </summary>
    public class GameObject
    {
        /// <summary>
        ///     Game object tag
        /// </summary>
        public string Tag { get; private set; }
        private IList<IComponent> _components;
        
        public TransformComponent TransformComponent { get; private set; }
                
        public GameObject(string tag, TransformComponent transformComponent)
        {
            Tag = tag;
            _components = new List<IComponent>();
            TransformComponent = transformComponent;
            TransformComponent.Parent = this; //TODO refactoring, add in component list
        }

        public void AddComponent(IComponent component)
        {
            if (_components == null) _components = new List<IComponent>();
            component.Parent = this;
            _components.Add(component);
        }

        /// <summary>
        ///     Get component if it contains in Game Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IComponent GetComponent<T>() where T: IComponent
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T)) return (T)component;
            }

            return null;
        }
        
        /// <summary>
        ///     Update state of the GameObject
        /// </summary>
        /// <param name="elapsed">Time elapsed since last update</param>
        /// <param name="world">Parent component of game object</param>
        public virtual void Update(float elapsed, IGameState world)
        {
            foreach (var component in _components)
            {
                component.Update(elapsed);
            }
            TransformComponent.Update(elapsed);
        }

        /// <summary>
        ///     Render game object on screen
        /// </summary>
        public virtual void Render()
        {
            foreach (var component in _components)
            {
                component.Render();
            }
        }
    }
}