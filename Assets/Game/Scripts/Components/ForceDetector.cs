using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class ForceDetector
    {
        [SerializeField] private Transform pushPoint;
        [SerializeField] private float checkRadius;

        public List<Rigidbody2D> GetPushingTargets()
        {
            var getPushingTargets = new List<Rigidbody2D>();
            
            var hits = Physics2D.OverlapCircleAll(pushPoint.position, checkRadius);
            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Rigidbody2D targetRb2d))
                {
                    getPushingTargets.Add(targetRb2d);
                }
            }
            return getPushingTargets;
        }
    }
}