using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class MoveAnimBehaviour : IEntityInit, IEntityDispose, IEntityUpdate
    {
        private const float MOVE_DURATION = 0.08f;
        private static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));

        private Animator _animator;
        private IEvent<Vector3> _moveEvent;

        private float _moveTime;

        public void Init(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            _moveEvent = entity.GetMoveEvent();
            _moveEvent.Subscribe(this.OnMoved);
        }

        public void Dispose(in IEntity entity)
        {
            _moveEvent.Unsubscribe(this.OnMoved);
        }

        public void OnUpdate(in IEntity entity, in float deltaTime)
        {
            if (_moveTime <= 0)
                return;

            _moveTime -= deltaTime;
            if (_moveTime <= 0)
                _animator.SetBool(IsMoving, false);
        }

        private void OnMoved(Vector3 _)
        {
            _animator.SetBool(IsMoving, true);
            _moveTime = MOVE_DURATION;
        }
    }
}