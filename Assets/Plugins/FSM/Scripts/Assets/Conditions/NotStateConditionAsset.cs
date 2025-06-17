using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.FSM
{
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class NotStateConditionAsset : IStateConditionAsset
    {
        [SerializeReference, HideLabel]
        private IStateConditionAsset condition = default;
        
        public Func<bool> Create(object context)
        {
            Func<bool> cond = this.condition.Create(context);
            return () => !cond.Invoke();
        }
    }
}