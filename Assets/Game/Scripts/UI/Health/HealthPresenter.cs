using System;
using Atomic.Entities;
using Modules.Common;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.UI
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private HealthScreen healthScreen;
        [SerializeField] private SceneEntity playerEntity;
        private Health _health;
        
        private void Awake()
        {
            _health = playerEntity.GetHealth();
        }

        private void OnEnable()
        {
            _health.OnStateChanged+=OnStateChanged;
            _health.OnHealthChanged+=OnHealthChanged;
        }

        private void OnDisable()
        {
            _health.OnStateChanged-=OnStateChanged;
            _health.OnHealthChanged-=OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            healthScreen.TakeDamage(health);
        }

        private void OnStateChanged()
        {
            var healthPercent = _health.GetPercent();
            healthScreen.ChangePercent(healthPercent);
        }
    }
}