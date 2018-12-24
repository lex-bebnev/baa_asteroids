
using System;
using OpenTK;

namespace Asteroids.Engine.Components
{
    public class PhysicsComponent: BaseComponent
    {
        public float AngularVelocity { get; set; }
        public float Velocity { get; set; }
        public float DragFactor { get; set; }
        
        private float vX;
        private float vY;
        
        public PhysicsComponent()
        {
            AngularVelocity = 0.0f;
            Velocity = 0.0f;
            DragFactor = 1.0f;
        }

        public override void Update(float elapsedTime)
        {
            ThrustObject(elapsedTime);
            RotateObject(elapsedTime);
            if(Math.Abs(Velocity) < 0.00000001f) DragObject(elapsedTime);
        }

        private void ThrustObject(float elapsedTime)
        {
            var delta = Parent.TransformComponent.Direction * (Velocity) * elapsedTime;
            Parent.TransformComponent.Position += delta;
            if (Math.Abs(Velocity) > 0.00000001f)
            {
                vX = delta.X;
                vY = delta.Y;
            }
        }

        private void RotateObject(float elapsedTime)
        {
            Parent.TransformComponent.Rotation += new Vector3(0.0f, 0.0f, (float) (AngularVelocity * elapsedTime));
            float
                xD = (float) (Math.Sin(Parent.TransformComponent.Rotation
                    .Z)); //2 * Math.PI * (actor.TransformComponent.Rotation.Z / 360.0f)));
            float
                yD = (float) (Math.Cos(Parent.TransformComponent.Rotation
                    .Z)); //2 * Math.PI * (actor.TransformComponent.Rotation.Z / 360.0f)));
            var d = new Vector3(xD, -yD, 0.0f);
            d.Normalize();
            Parent.TransformComponent.Direction = d;
        }

        private void DragObject(float elapsedTime)
        {
            Parent.TransformComponent.Position += new Vector3(vX, vY, 0.0f);
            vX = vX - vX * DragFactor;
            vY = vY - vY * DragFactor;
        }
    }
}