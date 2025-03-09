using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class RotateComponent : MonoBehaviour
    {
        [SerializeField] private Transform _rotationRoot;
        [SerializeField] private bool _canRotate;
        private Vector3 _rotateDirection;
        private AndCondition _andCondition = new();
        
        public void SetDirection(Vector3 direction)
        {
            _rotateDirection = direction;
        } 
        
        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if(!_canRotate || !_andCondition.IsTrue())
            {
                return;
            }

            if (_rotateDirection == Vector3.zero)
            {
                return;
            }
            transform.eulerAngles = new Vector3(0, _rotateDirection.x > 0 ? 0 : 180);
/*
            var targetRotation = Quaternion.LookRotation(_rotateDirection, Vector3.up);
            _rotationRoot.rotation = Quaternion.Lerp(_rotationRoot.rotation, targetRotation, _rotateRate);*/
        }
        
        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}