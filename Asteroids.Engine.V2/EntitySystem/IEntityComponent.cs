namespace Asteroids.Engine.V2.EntitySystem
{
    public interface IEntityComponent
    {
        uint EntityId { get; set; }
        bool Init();
    }
}