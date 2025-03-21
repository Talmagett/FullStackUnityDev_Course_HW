using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class RotateTowardsBehaviour : IEntityFixedUpdate
    {
        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            Vector3 direction = entity.GetAngularDirection().Value;
            RotateUseCase.RotateTowards(entity, direction, deltaTime);
        }
    }
}