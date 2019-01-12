using System;
using Asteroids.Engine.Utils;
using Asteroids.OGL.GameEngine.Managers;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Asteroids.OGL.GameEngine
{
    /// <inheritdoc />
    /// <summary>
    ///     Main engine class with Gameloop
    /// </summary>
    public abstract class BaseEngine: GameWindow
    {    
        protected GameClock Timer;
        
        public BaseEngine(int width, int height, string title) 
            : base(width, height, GraphicsMode.Default, title)
        {
            Console.WriteLine("Engine initialization...");
            
            InputManager.Initialize(this);
            InitializeStatesInternal();

            Timer = new GameClock();
            
            Console.WriteLine(GL.GetString(StringName.Version));
            
            Console.WriteLine("Engine initialization complete.");
        }
        
        #region Internal
        
        private void InitializeStatesInternal()
        {
            InitializeStates();
        }
        
        #endregion

        #region Abstract
    
        /// <summary>
        ///     Initialize states
        /// </summary>
        public abstract void InitializeStates();
        
        /// <summary>
        ///     Update game state
        /// </summary>
        /// <param name="elapsedMilliseconds">Time since last update</param>
        public abstract void Update(float elapsedMilliseconds);

        /// <summary>
        ///     Render game state
        /// </summary>
        public abstract void Render();

        #endregion
        
        #region Overrided OpenTK methods
        
        protected override void OnLoad(EventArgs e)
        {
            Renderer.SetupRenderer(this);
            
            base.OnLoad(e);
        }
       
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            InputManager.Update();
            
            Update(Timer.GetElaspedSeconds());
        }
        
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Render();
            
            SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            Renderer.Unload();
            base.OnUnload(e);
        }

        #endregion
    }
}