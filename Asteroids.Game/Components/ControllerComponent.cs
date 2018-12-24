using Asteroids.Engine.Components;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK.Input;

namespace Asteroids.Game.Components
{
    public class ControllerComponent : BaseComponent
    {
        private static float VELOCITY = 0.4f;
        private static float ANGULAR_VELOCITY = 3.0f;
        private static float DGAR_FACTOR = 0.005f;

        public override void Update(float elapsedTime)
        {
            PhysicsComponent physics = (PhysicsComponent) Parent.GetComponent<PhysicsComponent>(); //TODO Get before update
            if (physics == null) return;
            if (InputManager.KeyDown(Key.W))
            {
                physics.Velocity = VELOCITY;
                physics.DragFactor = 1.0f;
            }
            else
            {
                physics.DragFactor = DGAR_FACTOR;
                physics.Velocity = 0;
            }
            if (InputManager.KeyDown(Key.A))
                physics.AngularVelocity = ANGULAR_VELOCITY;
            else if (InputManager.KeyDown(Key.D))
                physics.AngularVelocity = -ANGULAR_VELOCITY;
            else physics.AngularVelocity = 0;
        }
    }
}