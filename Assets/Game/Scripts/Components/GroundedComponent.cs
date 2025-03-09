using System;
using UnityEngine;

namespace Game.Components
{
    public class GroundedComponent : MonoBehaviour
    {
        [SerializeField] private Transform groundPoint;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayerMask;
        
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, groundLayerMask);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundPoint.position, groundCheckRadius);
        }
    }
}