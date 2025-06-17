using System;
using System.Collections.Generic;

namespace Modules.FSM
{
    public interface IStateMachine<TKey> : IState
    {
        event Action<TKey> OnStateChanged;
        event Action<TKey> OnStateAdded;
        event Action<TKey> OnStateRemoved;

        TKey CurrentState { get; }
        int StateCount { get; }
        IReadOnlyCollection<TKey> AllStates { get; }

        bool TryChangeState(TKey key, Action transition = null);
        void ChangeState(TKey key, Action transition = null);
        
        void AddState(TKey key, IState state);
        bool RemoveState(TKey key);
        bool ContainsState(TKey key);
    }
    
    public interface IStateMachine : IStateMachine<Type>
    {
    }
}