using System.Linq;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components;
using Asteroids.Engine.Interfaces;
using OpenTK;

namespace Asteroids.Game.Components.EnemyComponents
{
    public class FollowingComponent: BaseComponent
    {
        private string _pursuedObjectTag;
        private GameObject _pursuedGameObject;
        private IGameState _gameWorld;
        
        public FollowingComponent(string pursuedObjectTag, IGameState gameWorld)
        {
            _pursuedObjectTag = pursuedObjectTag;
            _gameWorld = gameWorld;
            FindObject(_pursuedObjectTag);
        }

        private void FindObject(string pursuedObjectTag)
        {
            _pursuedGameObject = _gameWorld.GameObjects.First(item => item.Tag == pursuedObjectTag);
        }

        public override void Update(float elapsedTime)
        {
            if (!IsObjectExist()) return;
            
            Vector3 direction = _pursuedGameObject.TransformComponent.Position - Parent.TransformComponent.Position;
            direction.Normalize();
            Parent.TransformComponent.Direction = direction;
        }

        private bool IsObjectExist()
        {
            if (_pursuedGameObject != null) return true;
            
            FindObject(_pursuedObjectTag);
            
            return _pursuedGameObject != null;
        }
    }
}