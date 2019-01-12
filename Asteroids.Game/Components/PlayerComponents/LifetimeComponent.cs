using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;

namespace Asteroids.Game.Components.PlayerComponents
{
    public class LifetimeComponent: BaseComponent
    {
        private readonly float _timeToLive;
        private readonly IGameState _gameWorld;
        private float _liveTime;
        
        public LifetimeComponent(IGameState world, float timeToLive)
        {
            _timeToLive = timeToLive;
            _gameWorld = world;
            _liveTime = 0;
        }

        public override void Update(float elapsedTime)
        {
            _liveTime += elapsedTime;
            if (_liveTime > _timeToLive)
            {
                _gameWorld.GameObjects.Remove(Parent);
            }
        }
    }
}