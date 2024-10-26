using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroy;

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private int _damage;

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }
        
        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void SetVelocity(Vector3 velocity)
        {
            rigidbody2D.velocity = velocity;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Spaceship spaceship))
            {
                spaceship.TakeDamage(_damage);
                OnDestroy?.Invoke(this);
            }
        }
    }
}