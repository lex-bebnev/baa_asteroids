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
        private IInputComponent _inputComponent;
        private IPhysicsComponent _physicsComponent;
        private IGraphicsComponent _graphycsComponent;
        public TransformComponent TransformComponent { get; }
        public ModelComponent ModelComponent { get; }

        public GameObject(ModelComponent modelComponent, IInputComponent inputComponent, 
            IPhysicsComponent physicsComponent, IGraphicsComponent graphycsComponent)
        {
            _inputComponent = inputComponent;
            _physicsComponent = physicsComponent;
            _graphycsComponent = graphycsComponent;
            TransformComponent = new TransformComponent();
            ModelComponent = modelComponent;
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
            _graphycsComponent.Update(this, ModelComponent);
        }
    }
}