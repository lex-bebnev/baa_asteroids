using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Commands
{
    public class DebugCommand: ICommand
    {
        private string _commandName;
        
        
        public DebugCommand(string name)
        {
            _commandName = name;
        }

        public IStateComponent Execute(GameObject actor, float elapsedTime)
        {
            Console.WriteLine($"Debug command {_commandName} executed.");
            return null;
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}