using System;
using System.Collections.Generic;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class PushDetector : MonoBehaviour
    {
        [SerializeField] private Transform pushPoint;
        [SerializeField] private float checkRadius;
        private readonly AndCondition _andCondition = new();

        public IEnumerable<Rigidbody2D> GetPushingTargets()
        {
            var rigidbody2Ds = new List<Rigidbody2D>();
            
            if(!_andCondition.IsTrue())
                return rigidbody2Ds;
            
            var hits = Physics2D.OverlapCircleAll(pushPoint.position, checkRadius);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Rigidbody2D targetRb2d))
                {
                    rigidbody2Ds.Add(targetRb2d);
                }
            }
            return rigidbody2Ds;
        }
        
        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}