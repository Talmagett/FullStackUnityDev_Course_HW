using System;
using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    public class Spider : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private PushComponent pushComponent;
        [SerializeField] private Animator animator;
        [SerializeField] private int damage;
        
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        private void OnEnable()
        {
            healthComponent.OnTakeDamage += OnTakeDamage;
            healthComponent.OnDead += OnHealthEmpty;
        }

        private void OnDisable()
        {
            healthComponent.OnTakeDamage -= OnTakeDamage;
            healthComponent.OnDead -= OnHealthEmpty;
        }

        private void OnTakeDamage()
        {
            animator.SetTrigger(TakeDamage);
        }

        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.TryGetComponent(out IDamageable damageable)) return;
            damageable.TakeDamage(damage);

            if (!other.transform.TryGetComponent(out Rigidbody2D rigidbody2DTarget)) return;
            var dir = new Vector3(other.transform.position.x - transform.position.x, 0);
            dir.Normalize();
            pushComponent.Push(rigidbody2DTarget, dir);
        }
    }
}