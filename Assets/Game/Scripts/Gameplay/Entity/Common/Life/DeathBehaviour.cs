using System;
using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class DeathBehaviour : IEntityInit, IEntityDispose
    {
        private Health _health;
        private GameObject _gameObject;
        private BaseEvent _deathEvent;
        public void Init(in IEntity entity)
        {
            _gameObject = entity.GetGameObject();
            _health = entity.GetHealth();
            _deathEvent = entity.GetDeathEvent();
            _health.OnHealthEmpty+=OnHealthEmpty;
        }

        public void Dispose(in IEntity entity)
        {
            _health.OnHealthEmpty-=OnHealthEmpty;
        }
    
        private void OnHealthEmpty()
        {
            _deathEvent?.Invoke();
            _gameObject.SetActive(false);
        }
    }
}