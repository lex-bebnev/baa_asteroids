namespace Asteroids.Game.Utils
{
    public enum RenderModes
    {
        Polygons = 0,
        Sprites = 1
    } 
    
    public static class Settings
    {
        public static RenderModes RenderMode { get; set; } = RenderModes.Polygons;
    }
}