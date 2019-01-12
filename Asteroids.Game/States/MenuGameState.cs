using System;
using System.Collections.Generic;
using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;
using Asteroids.OGL.GameEngine.Managers;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;
using OpenTK.Input;

namespace Asteroids.Game.States
{
    public class MenuGameState: IGameState
    {
        public IList<GameObject> GameObjects { get; }
        public float[] GameWorldSize { get; } = { 800.0f, 600.0f};
        public string Name { get; }
        public bool IsReady { get; private set; } = false;

        public MenuGameState(string name)
        {
            Name = name;
            GameObjects = new List<GameObject>();
        }

        public void Load()
        {
            Console.WriteLine("Load game state...");
            
            IsReady = true;
            Console.WriteLine("Load gamestate complete");
        }

        public void AddGameObject(GameObject obj)
        {
            if(obj == null) return;
            GameObjects.Add(obj);
        }

        public void Update(float elapsedTime)
        {
            if (InputManager.KeyDown(Key.S))
            {
                
            }
            if (InputManager.KeyDown(Key.E))
            {
                
            }
        }

        public void Render()
        {
            Renderer.RenderText("Start - \"S\"", new Vector3(-45.0f, 50.0f, -1.0f), 1);
            Renderer.RenderText("Exit - \"E\"", new Vector3(-45.0f, 50.0f, -1.0f), 1);
        }

    }
}