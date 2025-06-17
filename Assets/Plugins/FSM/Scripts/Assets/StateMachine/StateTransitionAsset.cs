using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.FSM
{
    [Serializable]
    public sealed class StateTransitionAsset<TKey, TContext>
    {
        [HorizontalGroup]
        [LabelText("From")]
        [SerializeField]
        internal TKey _from;

        [HorizontalGroup]
        [LabelText("    To")]
        [SerializeField]
        internal TKey _to;
        
        [SerializeField]
        private int _priority;

        [SerializeReference]
        private IStateConditionAsset _condition = default;
        
        [SerializeReference]
        private IStateActionAsset _action = default;
        
        public StateTransition<TKey> Create(TContext context)
        {
            return new StateTransition<TKey>(
                _from,
                _to,
                _priority,
                _condition?.Create(context),
                _action?.Create(context)
            );
        }
    }
}