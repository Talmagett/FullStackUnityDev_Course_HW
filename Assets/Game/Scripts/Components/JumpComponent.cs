using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class JumpComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float jumpPower = 10f;
        [SerializeField] private bool canJump = true;

        private readonly AndCondition _andCondition = new();

        public void Jump()
        {
            if (!canJump || !_andCondition.IsTrue())
                return;

            rigidbody2D.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
        }

        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}