using System;
using System.Windows.Forms;
using OpenTK;

namespace Asteroids_clone
{
    public class Program
    {
        static void Main()
        {
            using (Game game = new Game(800, 600, "BAA-Asteroids"))
            {
                game.Run(60.0);
            }
        }
    }
}