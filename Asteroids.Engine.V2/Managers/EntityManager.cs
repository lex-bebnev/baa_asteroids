using System;
using System.Collections.Generic;
using Asteroids.Engine.V2.EntitySystem;

namespace Asteroids.Engine.V2.Managers
{
    public class EntityManager: IEntityManager
    {
        private List<IComponentSystem> _systems;
        private Dictionary<uint, Entity> _entities;
        private List<List<IEntityComponent>> _components;

        public EntityManager()
        {
            // TODO: Temp -> Create some test things
        }

        public void CreateSystems(){}
        public void Update(float deltaTime){}

        public uint CreateEntity()
        {
            throw new NotImplementedException();
        }

        public void DestroyEntity(uint entityId) {}
    }
}