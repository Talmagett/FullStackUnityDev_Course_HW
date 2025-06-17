using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.FSM
{
    public class StateMachineAsset<TKey, TContext> : StateAsset<TContext>
    {
        [Space]
        [ValueDropdown(nameof(DrawStateNames))]
        [SerializeField]
        protected int _initialState;

        [Header("States")]
        [SerializeField]
        protected StateInfo[] _states;

        public virtual IStateMachine<TKey> CreateStateMachine(TContext context)
        {
            var initialState = _states[_initialState].key;
            return new StateMachine<TKey>(initialState, _states.Select(it =>
                new KeyValuePair<TKey, IState>(it.key, it.state.CreateState(context))));
        }

        public override IState CreateState(TContext context) => this.CreateStateMachine(context);

        protected ValueDropdownList<int> DrawStateNames()
        {
            var result = new ValueDropdownList<int>();

            if (_states == null)
                return result;

            int i = 0;
            foreach (var state in _states)
                result.Add(new ValueDropdownItem<int>(state.key.ToString(), i++));

            return result;
        }

        [Serializable]
        protected struct StateInfo
        {
            [SerializeField]
            public TKey key;

            [SerializeField]
            public StateAsset<TContext> state;
        }
    }
}