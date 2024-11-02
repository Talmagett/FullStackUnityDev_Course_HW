using System;
using UnityEngine;

namespace ShootEmUp
{
    public class InputAdapter : MonoBehaviour
    {
        public event Action OnFire;
        public int MoveDirection { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                OnFire?.Invoke();

            if (Input.GetKey(KeyCode.LeftArrow))
                MoveDirection = -1;
            else if (Input.GetKey(KeyCode.RightArrow))
                MoveDirection = 1;
            else
                MoveDirection = 0;
        }
    }
}