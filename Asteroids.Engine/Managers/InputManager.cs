using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Engine.Managers
{
    /// <summary>
    ///     Input Manager for <see cref="M:OpenTK.GameWindow" />
    /// </summary>
    public static class InputManager
    {
        private static List<Key> KeysDown;
        private static List<Key> KeysDownLast;
        
        /// <summary>
        ///     Initialize Input Manager
        /// </summary>
        /// <param name="game">Game window - source of input events</param>
        public static void Initialize(GameWindow game)
        {
            KeysDown = new List<Key>();
            KeysDownLast = new List<Key>();
            
            game.KeyDown += GameOnKeyDown;
            game.KeyUp += GameOnKeyUp;
        }

        private static void GameOnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            while (KeysDown.Contains(e.Key))
            {
                KeysDown.Remove(e.Key);
            }
        }

        private static void GameOnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            KeysDown.Add(e.Key);
        }

        public static void Update()
        {
            KeysDownLast = new List<Key>(KeysDown);
        }

        public static bool KeyPress(Key key)
        {
            return (KeysDown.Contains(key) && !KeysDownLast.Contains(key));
        }

        public static bool KeyRelease(Key key)
        {
            return (!KeysDown.Contains(key) && KeysDownLast.Contains(key));
        }

        public static bool KeyDown(Key key)
        {
            return KeysDown.Contains(key);
        }
    }
}