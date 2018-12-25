using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Components
{
    public class TtlComponent: BaseComponent
    {
        private float _baseTTL;
        private IGameState _gameWorld;
        private float _liveTime;
        
        public TtlComponent(IGameState world, float baseTtl)
        {
            _baseTTL = baseTtl;
            _gameWorld = world;
            _liveTime = 0;
        }

        public override void Update(float elapsedTime)
        {
            _liveTime += elapsedTime;
            if (_liveTime > _baseTTL)
            {
                _gameWorld.GameObjects.Remove(Parent);
            }
        }
    }
}