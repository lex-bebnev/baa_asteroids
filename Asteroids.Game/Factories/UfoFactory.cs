﻿using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.EnemyComponents;
using Asteroids.Game.Components.PlayerComponents;
using Asteroids.OGL.GameEngine.Utils;
using OpenTK;

namespace Asteroids.Game.Factories
{
    public static class UfoFactory
    {
        private static readonly float[] UfoVertecies =
        {
            -0.5f, 0.0f, 0.0f,
            0.5f, 0.0f, 0.0f,
            -0.25f, -0.2f, 0.0f,
            0.25f, -0.2f, 0.0f,
            -0.25f, 0.2f, 0.0f,
            0.25f, 0.2f, 0.0f,
            -0.05f, 0.5f, 0.0f,
            0.05f, 0.5f, 0.0f
        };
        private static readonly uint[] UfoIndeces =
        {
            0, 1, 2,
            1, 2, 3,
            0, 4, 1,
            4, 5, 1,
            4, 6, 7,
            4, 5, 7
        };

        private static LoadResult? GpuBindedPolygonData;
        
        public static GameObject GetUfoGameObject(Vector3 coordinate, Vector3 scale, IGameState gameWotld)
        {
            if(!GpuBindedPolygonData.HasValue) 
                GpuBindedPolygonData = Renderer.LoadObject(UfoVertecies, UfoIndeces);
            
            GameObject ufo = new GameObject(
                "Ufo",
                new TransformComponent(coordinate, scale)
            );
            
            //ufo.AddComponent(new PolygonRenderComponent(GpuBindedPolygonData.Value.VAO, UfoIndeces.Length));
            ufo.AddComponent(new SpriteRendererComponent("ufo-2.png"));
            ufo.AddComponent(new PhysicsComponent());
            ufo.AddComponent(new UfoAiComponent(gameWotld));
            ufo.AddComponent(new BulletCollisionsComponent(gameWotld, 20.0f, 20.0f));
            ufo.AddComponent(new CoordinateComponent(gameWotld));

            return ufo;
        }
    }
}