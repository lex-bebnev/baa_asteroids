using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class ControllerComponent : BaseComponent
    {
        private static float VELOCITY = 150.0f;
        private static float ANGULAR_VELOCITY = 180.0f;
        private static float DGAR_FACTOR = 0.25f;

        private PhysicsComponent _physics;
        
        public override void Update(float elapsedTime)
        {
            
            if (_physics == null)
            {
                _physics = (PhysicsComponent) Parent.GetComponent<PhysicsComponent>(); //TODO Get before update
                if(_physics == null) return;
                _physics.DragFactor = DGAR_FACTOR;
            }
            
            HandleThrustInput();
            HandleRotateInput();
        }

        private void HandleThrustInput()
        {
            if(_physics == null) return;
            
            if (InputManager.KeyDown(Key.W))
                _physics.Velocity = VELOCITY;
            else
                _physics.Velocity = 0.0f;
        }

        private void HandleRotateInput()
        {
            if(_physics == null) return;
            if (InputManager.KeyDown(Key.A))
                _physics.AngularVelocity = ANGULAR_VELOCITY;
            else if (InputManager.KeyDown(Key.D))
                _physics.AngularVelocity = -ANGULAR_VELOCITY;
            else _physics.AngularVelocity = 0;
        }
    }
}