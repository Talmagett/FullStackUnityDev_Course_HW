using Atomic.Entities;
using SampleGame;
using UnityEngine;

namespace Game.Scripts.Gameplay.Entity.Common.TakeDamage
{
    public class TakeDamageAnimBehaviour: IEntityInit, IEntityDispose
    {
        private readonly int _takeDamageHash;
        private Animator _animator;

        public TakeDamageAnimBehaviour(string takeDamage)
        {
            _takeDamageHash = Animator.StringToHash(takeDamage);
        }

        public void Init(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetDamageTakenEvent().Subscribe(OnDamageTaken);
        }

        public void Dispose(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetDamageTakenEvent().Subscribe(OnDamageTaken);
        }

        private void OnDamageTaken(TakeDamageArgs takeDamageArgs)
        {
            _animator.SetTrigger(_takeDamageHash);
        }
    }
}