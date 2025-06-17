using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.FSM
{
    public class AutoStateMachineAsset<TKey, TContext> : StateMachineAsset<TKey, TContext>
    {
        [Header("Transitions")]
        [SerializeField]
        private StateTransitionAsset<TKey, TContext>[] _transitions;
        
        public override IStateMachine<TKey> CreateStateMachine(TContext context) => 
            this.CreateAutoStateMachine(context);

        public virtual IAutoStateMachine<TKey> CreateAutoStateMachine(TContext context)
        {
            var states = _states.Select(it => new KeyValuePair<TKey, IState>(it.key, it.state.CreateState(context)));
            var transitions = _transitions.Select(it => it.Create(context));
            TKey initialState = _states[_initialState].key;
            return new AutoStateMachine<TKey>(initialState, states, transitions);
        }

        // private void OnValidate()
        // {
        //     if (_transitions == null)
        //         return;
        //
        //     ValueDropdownList<int> dropdownList = this.DrawStateNames();
        //     foreach (StateTransitionAsset<TKey,TContext> transition in _transitions)
        //         transition.editor_stateNames = dropdownList;
        // }
    }
}