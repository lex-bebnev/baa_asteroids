namespace Asteroids.Game
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Main entry point in gameloop
            using (GameWindow gameWindow = new GameWindow(800, 600, "BAA-Asteroids"))
            {
                gameWindow.Run(60.0);
            }
        }
    }
}