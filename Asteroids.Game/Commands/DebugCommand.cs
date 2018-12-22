using System;
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

        public void Execute()
        {
            Console.WriteLine($"Debug command {_commandName} executed.");
        }

        public void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}