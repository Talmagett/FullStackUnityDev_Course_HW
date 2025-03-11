using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class PushComponent : MonoBehaviour
    {
        public event Action OnPush;
        [SerializeField] private float pushPower;
        

        public void Push(Rigidbody2D target, Vector3 direction)
        {
            OnPush?.Invoke();
            if (target == null)
                return;
            target.AddForce(direction*pushPower, ForceMode2D.Impulse);
        }
        
    }
}