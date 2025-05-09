using System;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class DeathAnimBehaviour : IEntityInit, IEntityDispose
    {
        private readonly int _deathHash;
        private Animator _animator;

        public DeathAnimBehaviour(string death)
        {
            _deathHash = Animator.StringToHash(death);
        }

        public void Init(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetDeathEvent().Subscribe(OnDeath);
        }

        public void Dispose(in IEntity entity)
        {
            _animator = entity.GetAnimator();
            entity.GetDeathEvent().Subscribe(OnDeath);
        }

        private void OnDeath()
        {
            _animator.SetTrigger(_deathHash);
        }
    }
}