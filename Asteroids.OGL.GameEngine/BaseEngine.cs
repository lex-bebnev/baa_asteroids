using System;
using Asteroids.Engine.Utils;
using Asteroids.OGL.GameEngine.Managers;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Asteroids.OGL.GameEngine
{
    public abstract class BaseEngine: GameWindow
    {    
        public int ScreenWidth { get { return this.Width; } }
        public int ScreenHeight { get { return this.Height; } }

        protected GameClock _timer;
        
        public BaseEngine(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            Console.WriteLine("Initialization...");
            
            InputManager.Initialize(this);
            //Renderer.SetupRenderer(this);
            InitializeStatesInternal();

            _timer = new GameClock();
            
            Console.WriteLine(GL.GetString(StringName.Version));
            
            Console.WriteLine("Initialization complete.");
        }

        protected override void OnLoad(EventArgs e)
        {
            Renderer.SetupRenderer(this);
            
            base.OnLoad(e);
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
        
        //TODO Try to subscribe to the event instead override
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            InputManager.Update();
            Update(_timer.GetElaspedMilliseconds());
        }
        
        //TODO Try to subscribe to the event instead override
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Render();
            
            SwapBuffers();
            base.OnRenderFrame(e);
        }
        #endregion
    }
}