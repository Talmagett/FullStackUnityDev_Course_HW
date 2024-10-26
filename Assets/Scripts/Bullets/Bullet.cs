using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet> OnDestroy;

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private bool _isPlayer;
        private int _damage;

        public void Init(Vector2 position, Color color, int physicsLayer, bool isPlayer, int damage)
        {
            transform.position = position;
            spriteRenderer.color = color;
            gameObject.layer = physicsLayer;
            _isPlayer = isPlayer;
            _damage = damage;
        }
        
        public void SetVelocity(Vector3 velocity)
        {
            rigidbody2D.velocity = velocity;
        }
        
        public void Activate()
        {
            this.gameObject.SetActive(true);
        }
        
        public void Deactivate()
        {
            this.gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Spaceship spaceship))
            {
                if (_isPlayer == spaceship.IsPlayer)
                    return;
                spaceship.TakeDamage(_damage);
                OnDestroy?.Invoke(this);
            }
        }
    }
}