using System;
using System.Collections.Generic;

namespace Modules.FSM
{
    public interface IAutoStateMachine<TKey> : IStateMachine<TKey>
    {
        event Action<StateTransition<TKey>> OnTransitionAdded;
        event Action<StateTransition<TKey>> OnTransitionRemoved;

        int TransitionCount { get; }
        IReadOnlyCollection<StateTransition<TKey>> AllTransitions { get; }

        bool AddTransition(StateTransition<TKey> transition);
        bool AddTransition(TKey from, TKey to, int priority, Func<bool> cond, Action action);

        bool RemoveTransition(StateTransition<TKey> transition);
        bool RemoveTransition(TKey from, TKey to);

        bool ContainsTransition(StateTransition<TKey> transition);
        bool ContainsTransition(TKey from, TKey to);
    }

    public interface IAutoStateMachine : IAutoStateMachine<Type>, IStateMachine
    {
    }
}