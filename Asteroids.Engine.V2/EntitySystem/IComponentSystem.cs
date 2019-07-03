namespace Asteroids.Engine.V2.EntitySystem
{
    public interface IComponentSystem
    {
        bool Init();
        void Update(float deltaTime);
    }
}