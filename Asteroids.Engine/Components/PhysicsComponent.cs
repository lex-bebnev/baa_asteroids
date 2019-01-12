
using System;
using OpenTK;

namespace Asteroids.Engine.Components
{
    public class PhysicsComponent: BaseComponent
    {
        public float AngularVelocity { get; set; }
        public float Velocity { get; set; }
        public float DragFactor { get; set; }
        
        private float _vX;
        private float _vY;
        
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
            if(Math.Abs(Velocity) < 0.00000001f) 
                DragObject(elapsedTime);
        }

        private void ThrustObject(float elapsedTime)
        {
            Vector3 delta = Parent.TransformComponent.Direction * (Velocity) * elapsedTime;
            Parent.TransformComponent.Position += delta;
            
            if (!(Math.Abs(Velocity) > 0.00000001f)) return;
            _vX = delta.X;
            _vY = delta.Y;
        }

        private void RotateObject(float elapsedTime)
        {
            Parent.TransformComponent.Rotation += new Vector3(0.0f, 0.0f, (AngularVelocity * elapsedTime));

            float radians = MathHelper.DegreesToRadians(Parent.TransformComponent.Rotation.Z);
            
            float
                xD = (float) (Math.Sin(radians));
            float
                yD = (float) (Math.Cos(radians));
            
            Vector3 d = new Vector3(xD, -yD, 0.0f);
            
            d.Normalize();
            Parent.TransformComponent.Direction = d;
        }

        private void DragObject(float elapsedTime)
        {
            Parent.TransformComponent.Position += new Vector3(_vX, _vY, 0.0f);
            _vX = _vX - _vX * (DragFactor*elapsedTime);
            _vY = _vY - _vY * (DragFactor*elapsedTime);
        }
    }
}