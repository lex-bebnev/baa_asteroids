using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using Asteroids.Game.Components.CommonComponents;
using Asteroids.Game.Components.PlayerComponents;
using Asteroids.Game.Utils;
using OpenTK;

namespace Asteroids.Game.Factories
{
    public static class PlayerFatory
    {
        private static float[] ShipVertices =
        {
            //Position          
            0.0f,   0.25f, -1.0f, 
            -0.25f,   0.4f, -1.0f, 
            0.25f,   0.4f, -1.0f, 
            0.0f,  -0.4f, -1.0f
        };

        private static uint[] ShipIndices =
        {
            0, 1, 3,
            0, 3, 2
        };
        
        public static GameObject GetPlayer(IGameState gameWorld)
        {
            TransformComponent transform = new TransformComponent(new Vector3(0.0f, 0.0f, -2.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                new Vector3(45.0f, 45.0f, 1.0f),
                new Vector3(0.0f, 0.0f, 0.0f));

            transform.Size = new Vector2(0.5f * transform.Scale.X, 0.5f * transform.Scale.Y);
            
            GameObject player = new GameObject(
                "Player", transform
                );
            
            if(Settings.RenderMode == RenderModes.Polygons) player.AddComponent(new PolygonRenderComponent(ShipVertices, ShipIndices));     
            else player.AddComponent(new SpriteRendererComponent("ship-1.png"));
            
            //player.AddComponent(new ControllerComponent());
            player.AddComponent(new PlayerControllerComponent());
            player.AddComponent(new PhysicsComponent(){DragFactor = 0.25f});
            player.AddComponent(new GunComponent(gameWorld));
            player.AddComponent(new CoordinateComponent(gameWorld));
            player.AddComponent(new PlayerStateComponent());
            
            
            return player;
        }
    }
}