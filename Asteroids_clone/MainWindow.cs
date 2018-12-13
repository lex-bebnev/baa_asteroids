using OpenTK;
using OpenTK.Graphics;

namespace Asteroids_clone
{
    public sealed class MainWindow: GameWindow
    {
        public MainWindow()
            : base(1280,
                720,
                GraphicsMode.Default,
                "Asteroids",
                GameWindowFlags.Default,
                DisplayDevice.Default,
                4,
                0,
                GraphicsContextFlags.ForwardCompatible)
        {
            //Title += ": OpenGL Version: " + GL.GetString(StringName.Version);
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            Title = $" (Vsync: {VSync}) FPS: {1f / e.Time:0}";

            SwapBuffers();
        }
    }
}