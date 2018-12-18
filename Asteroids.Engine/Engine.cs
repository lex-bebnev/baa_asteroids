using System;
using Asteroids.Engine.Managers;
using Asteroids.Engine.Utils;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace Asteroids.Engine
{
    public abstract class Engine: GameWindow
    {    
        public int ScreenWidth { get { return this.ClientSize.Width; } }
        public int ScreenHeight { get { return this.ClientSize.Height; } }

        protected GameClock _timer;
        
        public Engine(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {
            Console.WriteLine("Initialization...");
            
            InputManager.Initialize(this);
            InitializeStatesInternal();

            _timer = new GameClock();
            
            Console.WriteLine("Initialization complete.");
        }

        #region Internal
        
        private void InitializeStatesInternal()
        {
            InitializeStates();
        }
        
        #endregion
        
        /// <summary>
        ///     Initialize states
        /// </summary>
        public abstract void InitializeStates();
        /// <summary>
        ///     Update game state
        /// </summary>
        /// <param name="elapsedMilliseconds">Time since last update</param>
        public abstract void Update(float elapsedMilliseconds);


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
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            //Render function in game state
            
            SwapBuffers();
        }
        #endregion
    }
}