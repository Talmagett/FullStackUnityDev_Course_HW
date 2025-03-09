using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_rigidbody2D;
        [SerializeField] private float speed = 3f;
        [SerializeField] private Vector3 moveDirection;
        [SerializeField] private bool canMove = true;

        private readonly AndCondition _andCondition = new();

        private void Update()
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

            m_rigidbody2D.velocity = new Vector2(moveDirection.x * speed, m_rigidbody2D.velocity.y);
        }

        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}