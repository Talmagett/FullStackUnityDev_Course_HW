using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class RotateToTargetBehaviour : IEntityInit, IEntityFixedUpdate, IEntityDispose
    {
        private  IReactiveVariable<IEntity> _targetEntity;

        public void Init(in IEntity entity)
        {
            _targetEntity = entity.GetTarget();
        }

        public void Dispose(in IEntity entity)
        {
            _targetEntity = null;
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            IReactiveVariable<Vector3> direction = entity.GetAngularDirection();
            if(_targetEntity.Value==null)
            {
                direction.Value = Vector3.zero;
                return;
            }
            direction.Value = _targetEntity.Value.GetTransform().position - entity.GetTransform().position;
            direction.Value.Normalize();
            RotateUseCase.RotateTowards(entity, direction.Value, deltaTime);
        }
    }
}