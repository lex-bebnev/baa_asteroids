using System;

namespace Asteroids_clone
{

    public class Program
    {
        [STAThread]
        static void Main()
        {
            new MainWindow().Run(60);
        }                
    }
}