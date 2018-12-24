using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Components
{
    public class ColliderPhysicsComponent: IComponent
    {
        public GameObject Parent { get; set; }
        public void Update(float elapsedTime)
        {
            throw new System.NotImplementedException();
        }

        public void Render()
        {
            throw new System.NotImplementedException();
        }
    }
}