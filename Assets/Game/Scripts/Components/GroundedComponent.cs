using System;
using UnityEngine;

namespace Game.Components
{
    public class GroundedComponent : MonoBehaviour
    {
        [SerializeField] private Transform groundPoint;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayerMask;
        
        private bool _isGrounded;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundPoint.position, groundCheckRadius);
        }
        public bool IsGrounded() => _isGrounded;
        //
        private void FixedUpdate()
        {
            var hit = Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, groundLayerMask);
            if (hit)
            {
                _isGrounded = true;
                if (hit.CompareTag("Platform"))
                    transform.parent = hit.transform;    
                return;
            }
            _isGrounded = false;
            transform.parent = null;
        }
    }
}