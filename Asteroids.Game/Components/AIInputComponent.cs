using System;
using Asteroids.Engine.Common;
using Asteroids.Engine.Components.Interfaces;

namespace Asteroids.Game.Components
{
    public class AIInputComponent: IInputComponent
    {
        public void Update(GameObject obj)
        {
            obj.TransformComponent.Rotation.Z += 0.1f * 0.1f;
        }
    }
}