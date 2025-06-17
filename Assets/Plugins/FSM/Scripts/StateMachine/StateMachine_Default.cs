using System;
using System.Collections.Generic;
using System.Linq;

namespace Modules.FSM
{
    public sealed class StateMachine : StateMachine<Type>, IStateMachine
    {
        public StateMachine()
        {
        }

        public StateMachine(Type initialState, params IState[] states) :
            base(initialState, states.Select(it => new KeyValuePair<Type, IState>(it.GetType(), it)))
        {
        }
        
        public StateMachine(Type initialState, IEnumerable<IState> states) :
            base(initialState, states.Select(it => new KeyValuePair<Type, IState>(it.GetType(), it)))
        {
        }
    }
}