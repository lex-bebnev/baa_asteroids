using Asteroids.Engine.V2.EntitySystem;

namespace Asteroids.Engine.V2.Components
{
    public class TransformComponent: IEntityComponent
    {
        public uint EntityId { get; set; }
        public bool Init()
        {
            throw new System.NotImplementedException();
        }
    }
}