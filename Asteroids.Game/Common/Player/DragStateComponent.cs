using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.Common.Player
{
    public class DragStateComponent: IStateComponent
    {
        private float dragFactor;
        private Vector3 previousDirection;

        public float vX { get; set; }
        public float vY { get; set; }
        
        public DragStateComponent(float dragFactor, float baseX, float baseY)
        {
            this.dragFactor = dragFactor;
            vX = baseX;
            vY = baseY;
        }

        public IStateComponent HandleInput()
        {
            if (InputManager.KeyDown(Key.W))
            {
                return new ThrustStateComponent(0.3f);
            }
            if (InputManager.KeyDown(Key.A))
            {
                return new RotatingStateComponent(RotateDirection.Right, 3.0f);
            }
            if (InputManager.KeyDown(Key.D))
            {
                return new RotatingStateComponent(RotateDirection.Left, 3.0f);
            }
            return null;
        }

        public void Update(GameObject obj, float elapsedTime)
        {
                obj.TransformComponent.Position += new Vector3(vX, vY, 0.0f);
                vX = vX - vX * dragFactor;
                vY = vY - vY * dragFactor;
        }
    }
}