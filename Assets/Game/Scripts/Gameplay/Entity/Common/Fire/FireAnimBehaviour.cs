using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class FireAnimBehaviour : IEntityInit, IEntityDispose
    {
        private readonly int _fireHash;
        private Animator _animator;

        public FireAnimBehaviour(string fire)
        {
            _fireHash = Animator.StringToHash(fire);
        }

        public void Init(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetFireEvent().Subscribe(OnFire);
        }

        public void Dispose(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetFireEvent().Subscribe(OnFire);
        }

        private void OnFire()
        {
            _animator.SetTrigger(_fireHash);
        }
    }
}