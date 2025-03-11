using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    [System.Serializable]
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_rigidbody2D;
        [SerializeField] private float speed = 3f;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool moveY;

        private readonly AndCondition _andCondition = new();

        private void FixedUpdate()
        {
            Move();
        }

        public void SetDirection(Vector3 direction)
        {
            moveDirection = direction;
        }

        private void Move()
        {
            if (!canMove || !_andCondition.IsTrue())
                return;
            
            Vector2 targetVelocity = new Vector2(moveDirection.x * speed, 
                moveY ? moveDirection.y * speed : m_rigidbody2D.velocity.y);

            m_rigidbody2D.velocity = Vector2.Lerp(m_rigidbody2D.velocity, targetVelocity, Time.deltaTime * 10);
        }

        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}