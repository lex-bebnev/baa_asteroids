using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Commands;
using Asteroids.Game.InputHandlers;
using Asteroids.Game.InputHandlers.Player;
using OpenTK;

namespace Asteroids.Game.Common.Player
{
    public class DragState: IStateComponent
    {
        private float dragFactor;
        private Vector3 previousDirection;

        public float vX { get; set; }
        public float vY { get; set; }
        
        public IInputHandler InputHandler { get; }

        public DragState(float dragFactor, float baseX, float baseY)
        {
            this.dragFactor = dragFactor;
            vX = baseX;
            vY = baseY;
            InputHandler = new DragStateInputHandler();
        }

        public void Update(GameObject obj, float elapsedTime)
        {
            InputHandler.HandleInput();
            
            obj.TransformComponent.Position += new Vector3(vX, vY, 0.0f);
            vX = vX - vX * dragFactor;
            vY = vY - vY * dragFactor;
        }
    }
}