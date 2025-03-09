using UnityEngine;

namespace Game.Components
{
    public class DetectGroundComponent : MonoBehaviour
    {
        [SerializeField] private Transform groundPoint;
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private LayerMask groundLayerMask;
        
        public bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundPoint.position, groundCheckRadius, groundLayerMask);
        }
    }
}