using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class PushComponent : MonoBehaviour
    {
        public event Action OnPush;
        [SerializeField] private float pushPower;
        
        [SerializeField] private Transform pushPoint;
        [SerializeField] private float checkRadius;
        
        private readonly AndCondition _andCondition = new();

        public void Push(Vector3 direction)
        {
            if(!_andCondition.IsTrue())
                return;
            
            var hits = Physics2D.OverlapCircleAll(pushPoint.position, checkRadius);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Rigidbody2D targetRb2d))
                {
                    targetRb2d.AddForce(direction*pushPower,ForceMode2D.Impulse);
                }
            }
            OnPush?.Invoke();
        }
        
        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}