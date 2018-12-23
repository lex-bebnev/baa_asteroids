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
        
        private IPhysicsComponent _physicsComponent;
        private IGraphicsComponent _graphycsComponent;
        private IStateComponent _stateComponent;
        
        public TransformComponent TransformComponent { get; }
        public IStateComponent StateComponent
        {
            get { return _stateComponent;} 
            set
            {
                if (value == null) return;
                _stateComponent = value;
            } 
        }

        public GameObject(string tag,
            IPhysicsComponent physicsComponent, IGraphicsComponent graphycsComponent,
            TransformComponent transformComponent, IStateComponent stateComponent)
        {
            Tag = tag;
            _physicsComponent = physicsComponent;
            _graphycsComponent = graphycsComponent;
            TransformComponent = transformComponent;
            _stateComponent = stateComponent;
        }

        /// <summary>
        ///     Update state of the GameObject
        /// </summary>
        /// <param name="elapsed">Time elapsed since last update</param>
        /// <param name="world">Parent component of game object</param>
        public virtual void Update(float elapsed, IGameState world)
        {
            IStateComponent newState = _stateComponent?.HandleInput();
            if (newState != null) _stateComponent = newState;
            _stateComponent?.Update(this, elapsed);
            _physicsComponent?.Update(this, world);
        }

        /// <summary>
        ///     Render game object on screen
        /// </summary>
        public virtual void Render()
        {
            _graphycsComponent?.Update(this);
        }
    }
}