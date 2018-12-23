using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.Common.Player
{
    public class DragPlayerState: IStateComponent
    {
        private float dragFactor;
        private Vector3 previousDirection;

        public float vX { get; set; }
        public float vY { get; set; }
        
        public DragPlayerState(float dragFactor, float baseX, float baseY)
        {
            this.dragFactor = dragFactor;
            vX = baseX;
            vY = baseY;
        }

        public IStateComponent HandleInput()
        {
            if (InputManager.KeyDown(Key.W))
            {
                return new PlayerBaseState(0.5f);
            }
            if (InputManager.KeyDown(Key.A))
            {
                return null;//rightRotateCommand; 
            }
            if (InputManager.KeyDown(Key.D))
            {
                return null;//leftRotateCommand; 
            }
            if (InputManager.KeyDown(Key.Space))
            {
                return null;//fireCommand; 
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