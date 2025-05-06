using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveAnimBehaviour : IEntityInit, IEntityUpdate
    {
        private readonly int _isMovingHash;
        private Animator _animator;

        public MoveAnimBehaviour(string isMovingHash)
        {
            _isMovingHash = Animator.StringToHash(isMovingHash);
        }

        public void Init(in IEntity entity)
        {
            _animator = entity.GetAnimator();
        }

        public void OnUpdate(in IEntity entity, in float deltaTime)
        {
            _animator.SetBool(_isMovingHash, MoveUseCase.IsMoving(entity));
        }
    }
}