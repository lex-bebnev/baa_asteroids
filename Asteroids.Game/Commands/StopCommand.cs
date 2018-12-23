using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Common.Player;

namespace Asteroids.Game.Commands
{
    public class StopCommand: ICommand
    {
        public IStateComponent Execute(GameObject actor, float elapsedTime)
        {
            var delta = (actor.TransformComponent.Direction * 0.5f * elapsedTime);
            var state = new DragState(1.0f, delta.X, delta.Y);
            return state;       
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}