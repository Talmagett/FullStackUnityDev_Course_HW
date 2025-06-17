using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.BehaviourTree
{
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class BTConditionAsset_Not : IBTConditionAsset
    {
        [SerializeReference, HideLabel]
        private IBTConditionAsset condition = default;
        
        public Func<bool> Create(object context)
        {
            Func<bool> cond = this.condition.Create(context);
            return () => !cond.Invoke();
        }
    }
}