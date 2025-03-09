using System;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class ConditionComponent : MonoBehaviour
    {
        protected AndCondition AndCondition = new();
        
        public void AddCondition(Func<bool> condition)
        {
            AndCondition.AddCondition(condition);
        }
    }
}