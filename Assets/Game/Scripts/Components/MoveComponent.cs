using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Transform root;
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

            root.position += moveDirection * (speed * Time.deltaTime);
        }

        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}