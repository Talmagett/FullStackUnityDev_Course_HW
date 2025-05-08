using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveToTargetBehaviour : IEntityInit, IEntityFixedUpdate, IEntityDispose
    {
        private  IReactiveVariable<IEntity> _targetEntity;

        public void OnInit(in IEntity entity)
        {
            _targetEntity = entity.GetTargetEntity();
            _targetEntity.OnValueChanged += OnTargetEntityChanged;
        }
        private void OnTargetEntityChanged(IEntity targetEntity)
        {
            if (targetEntity == null)
            {
                return;
            }

            IReactiveVariable<Vector3> direction = targetEntity.GetPosition();
            entity.GetMoveDirection().Value = direction.Value - entity.GetPosition().Value;
        }
        
        public void OnDispose(in IEntity entity)
        {
            _targetEntity.OnValueChanged -= OnTargetEntityChanged;
            _targetEntity.Dispose();
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            IReactiveVariable<Vector3> direction = entity.GetMoveDirection();
            MoveUseCase.MoveTowards(entity, direction.Value, deltaTime);
        }
    }
}