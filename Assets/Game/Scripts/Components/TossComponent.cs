using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class TossComponent : MonoBehaviour
    {
        public event Action OnToss;
        private readonly AndCondition _andCondition = new();

        public void Toss()
        {
            
        }
        
        public void AddCondition(Func<bool> condition)
        {
            _andCondition.AddCondition(condition);
        }
    }
}