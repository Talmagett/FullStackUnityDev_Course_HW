using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class JumpComponent : MonoBehaviour
    {
        public event Action OnJump;
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float jumpPower = 10f;
        [SerializeField] private bool canJump = true;
        [SerializeField] private Cooldown cooldown;

        private readonly AndCondition _andCondition = new();

        private void Update()
        {
            cooldown.TickUpdate(Time.deltaTime);
        }
        
        public void Jump()
        {
            if (!canJump || !_andCondition.IsTrue()|| !cooldown.IsReady())
                return;

            rigidbody2D.AddForce(Vector2.up*jumpPower,ForceMode2D.Impulse);
            OnJump?.Invoke();
            cooldown.Reload();
        }

        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}