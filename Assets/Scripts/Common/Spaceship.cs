using System;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public class Spaceship : MonoBehaviour
    {
        public Action OnHealthEmpty;

        [field: SerializeField] public bool IsPlayer { get; private set; }
        [field: SerializeField] public Transform FirePoint { get; private set; }
        [field: SerializeField] public int Health { get; private set; }

        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] public float speed = 5.0f;

        public void TakeDamage(int damage)
        {
            if (Health <= 0)
                return;

            if (damage <= 0)
                return;

            Health = Mathf.Max(0, Health - damage);
            if (Health == 0)
                OnHealthEmpty?.Invoke();
        }
        

        public void Move(Vector2 moveDirection)
        {
            Vector2 moveStep = moveDirection * (Time.fixedDeltaTime * speed);
            var nextPosition = rigidbody2D.position + moveStep;
            rigidbody2D.MovePosition(nextPosition);
        }
        
        public void Fire(Vector2 position, Vector2 direction)
        {
            
        }
    }
}