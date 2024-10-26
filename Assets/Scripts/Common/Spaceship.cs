using System;
using ShootEmUp;
using UnityEngine;

namespace ShootEmUp
{
    public class Spaceship : MonoBehaviour
    {
        public event Action OnHealthEmpty;
        [field: SerializeField] public BulletData SpaceshipBulletData { get; private set; }
        
        [SerializeField] private int health;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] public float speed = 5.0f;
        
        private int _health;

        private void Awake()
        {            
            _health = health;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _health = health;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage(int damage)
        {
            if (_health <= 0)
                return;

            if (damage <= 0)
                return;

            _health = Mathf.Max(0, _health - damage);
            if (_health == 0)
                OnHealthEmpty?.Invoke();
        }

        public void Move(Vector2 moveDirection)
        {
            Vector2 moveStep = moveDirection * (Time.fixedDeltaTime * speed);
            var nextPosition = rigidbody2D.position + moveStep;
            rigidbody2D.MovePosition(nextPosition);
        }

        public void Fire(BulletManager bulletManager, Vector2 direction)
        {
            bulletManager.SpawnBullet(
                SpaceshipBulletData.FirePoint.position,
                SpaceshipBulletData.BulletColor,
                (int) SpaceshipBulletData.Layer,
                SpaceshipBulletData.BulletDamage,
                direction * SpaceshipBulletData.BulletSpeed
            );
        }

        [System.Serializable]
        public class BulletData
        {
            [field: SerializeField] public Transform FirePoint { get; private set; }
            [field: SerializeField] public int BulletDamage {get; private set;}
            [field: SerializeField] public float BulletSpeed {get; private set;}
            [field: SerializeField] public Color BulletColor {get; private set;}
            [field: SerializeField] public PhysicsLayer Layer {get; private set;}
        }
    }
}