namespace Asteroids.Engine.V2.EntitySystem
{
    public interface IEntityManager
    {
        void CreateSystems();
        void Update(float deltaTime);

        uint CreateEntity();
        void DestroyEntity(uint entityId);
    }
}