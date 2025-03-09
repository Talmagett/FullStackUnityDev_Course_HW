using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Components
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        public event Action OnDead;
        public event Action OnTakeDamage;

        [SerializeField] private int maxHealth;
        [SerializeField] private int currentHealth;
        [SerializeField] private bool _isDead;

        [Button]
        public void TakeDamage(int damage)
        {
            if (_isDead)
                return;

            currentHealth -= damage;
            OnTakeDamage?.Invoke();
            
            if (currentHealth <= 0)
            {
                _isDead = true;
                OnDead?.Invoke();
            }
        }

        public bool IsAlive()
        {
            return !_isDead;
        }
    }
}