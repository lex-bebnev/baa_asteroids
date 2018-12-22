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
        
        private IInputComponent _inputComponent;
        private IPhysicsComponent _physicsComponent;
        private IGraphicsComponent _graphycsComponent;
        public TransformComponent TransformComponent { get; }

        public GameObject(string tag, IInputComponent inputComponent,
            IPhysicsComponent physicsComponent, IGraphicsComponent graphycsComponent)
        {
            Tag = tag;
            _inputComponent = inputComponent;
            _physicsComponent = physicsComponent;
            _graphycsComponent = graphycsComponent;
            TransformComponent = new TransformComponent();
        }

        /// <summary>
        ///     Update state of the GameObject
        /// </summary>
        /// <param name="elapsed">Time elapsed since last update</param>
        /// <param name="world">Parent component of game object</param>
        public virtual void Update(float elapsed, IGameState world)
        {
            _inputComponent.Update(this);
            _physicsComponent.Update(this, world);
        }

        public virtual void Render()
        {
            _graphycsComponent.Update(this);
        }
    }
}