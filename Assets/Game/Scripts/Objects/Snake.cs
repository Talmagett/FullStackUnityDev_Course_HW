using System;
using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private PushComponent pushComponent;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource takeDamageSource;
        
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
            takeDamageSource.Play();
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
            pushComponent.Push(rigidbody2DTarget, Vector3.up);
        }
    }
}