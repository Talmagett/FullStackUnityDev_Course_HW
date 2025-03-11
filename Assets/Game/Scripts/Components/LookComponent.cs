using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class LookComponent : MonoBehaviour
    {
        [SerializeField] private Transform rotationRoot;
        [SerializeField] private Rigidbody2D rigidbody2DTarget;
        
        [SerializeField] private bool canRotate=true;
        private Vector3 _rotateDirection;
        private readonly AndCondition _andCondition = new();
        
        private void Update()
        {
            _rotateDirection = rigidbody2DTarget.velocity;
            Rotate();
        }

        private void Rotate()
        {
            if(!canRotate || !_andCondition.IsTrue())
            {
                return;
            }

            if (_rotateDirection == Vector3.zero||_rotateDirection.x == 0)
            {
                return;
            }
            
            rotationRoot.eulerAngles = new Vector3(0, _rotateDirection.x >0?0:180);
        }
        
        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}