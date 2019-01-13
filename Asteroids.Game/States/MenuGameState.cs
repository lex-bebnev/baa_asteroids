using System;
using System.Collections.Generic;
using Asteroids.Engine.Common;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Utils;
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

        public delegate void SelectHandler(string selectedMenu);
        public event SelectHandler Select; 
        
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

        public void RemoveGameObject(GameObject obj)
        {
            GameObjects.Remove(obj);
        }

        public void Update(float elapsedTime)
        {
            if (InputManager.KeyPress(Key.S))
            {
                Select?.Invoke("Start");
                Console.WriteLine("Start game");
            }
            if (InputManager.KeyPress(Key.E))
            {
                Select?.Invoke("Exit");
                Console.WriteLine("Exit");
            }
            if (InputManager.KeyPress(Key.Tab))
            {
                Settings.RenderMode = Settings.RenderMode == RenderModes.Polygons
                    ? RenderModes.Sprites
                    : RenderModes.Polygons;
                Console.WriteLine("Switch render mode");
            }
        }

        public void Render()
        {
            Renderer.RenderText("Start - \"S\"", new Vector3(-370.0f, 50.0f, -1.0f), 1);
            Renderer.RenderText($"Switch render model <{Enum.GetName(typeof(RenderModes), Settings.RenderMode)}> - \"Tab\"", new Vector3(-370.0f, 25.0f, -1.0f), 1);
            Renderer.RenderText("Exit - \"E\"", new Vector3(-370.0f, 0.0f, -1.0f), 1);
        }
    }
}