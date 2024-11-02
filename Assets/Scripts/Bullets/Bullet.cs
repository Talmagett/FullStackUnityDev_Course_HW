using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroy;
        public Vector3 Position=>transform.position;
        
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

        public void SetLayer(int layer)
        {
            gameObject.layer = layer;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Spaceship spaceship))
            {
                spaceship.HealthComponent.TakeDamage(_damage);
                OnDestroy?.Invoke(this);
            }
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}