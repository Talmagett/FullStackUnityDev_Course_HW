using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Modules.FSM
{
    public class AutoStateMachine<TKey> : StateMachine<TKey>, IAutoStateMachine<TKey>
    {
        public event Action<StateTransition<TKey>> OnTransitionAdded;
        public event Action<StateTransition<TKey>> OnTransitionRemoved;
        
        public int TransitionCount => _transitions.Count;
        public IReadOnlyCollection<StateTransition<TKey>> AllTransitions => _transitions;

        [ShowInInspector, ReadOnly]
        private readonly List<StateTransition<TKey>> _transitions;

        public AutoStateMachine()
        {
            _transitions = new List<StateTransition<TKey>>();
        }

        public AutoStateMachine(
            TKey initialState,
            IEnumerable<(TKey, IState)> states,
            IEnumerable<StateTransition<TKey>> transitions
        ) : base(initialState, states)
        {
            _transitions = new List<StateTransition<TKey>>(transitions);
            _transitions.Sort();
        }

        public AutoStateMachine(
            TKey initialState,
            IEnumerable<KeyValuePair<TKey, IState>> states,
            IEnumerable<StateTransition<TKey>> transitions
        ) : base(initialState, states)
        {
            _transitions = new List<StateTransition<TKey>>(transitions);
            _transitions.Sort();
        }

        public bool ContainsTransition(StateTransition<TKey> transition)
        {
            return _transitions.Contains(transition);
        }

        public bool ContainsTransition(TKey from, TKey to)
        {
            return this.FindTransition(from, to, out _);
        }

        public bool AddTransition(StateTransition<TKey> transition)
        {
            if (_transitions.Contains(transition))
                return false;

            _transitions.Add(transition);
            _transitions.Sort();

            this.OnTransitionAdded?.Invoke(transition);
            return true;
        }

        public bool AddTransition(TKey from, TKey to, int priority, Func<bool> cond, Action action)
        {
            if (this.ContainsTransition(from, to))
                return false;

            var transition = new StateTransition<TKey>(from, to, priority, cond, action);

            _transitions.Add(transition);
            _transitions.Sort();

            this.OnTransitionAdded?.Invoke(transition);
            return true;
        }

        public bool RemoveTransition(StateTransition<TKey> transition)
        {
            if (!_transitions.Remove(transition))
                return false;
            
            this.OnTransitionRemoved?.Invoke(transition);
            return true;
        }

        public bool RemoveTransition(TKey from, TKey to)
        {
            return this.FindTransition(from, to, out StateTransition<TKey> transition) &&
                   this.RemoveTransition(transition);
        }

        private bool FindTransition(TKey from, TKey to, out StateTransition<TKey> result)
        {
            for (int i = 0, count = _transitions.Count; i < count; i++)
            {
                StateTransition<TKey> transition = _transitions[i];
                if (transition.Equals(from, to))
                {
                    result = transition;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public override void OnUpdate(float deltaTime)
        {
            this.UpdateTransitions();
            base.OnUpdate(deltaTime);
        }
        
        private void UpdateTransitions()
        {
            if (_transitions.Count <= 0)
                return;

            for (int i = 0, count = _transitions.Count; i < count; i++)
            {
                StateTransition<TKey> transition = _transitions[i];
                if (!transition.from.Equals(_currentState.Key))
                    continue;

                if (!transition.CanPerform())
                    continue;

                this.ChangeState(transition.To, transition.Perform);
                break;
            }
        }
    }

    public sealed class AutoStateMachine : AutoStateMachine<Type>, IAutoStateMachine
    {
        public AutoStateMachine(
            Type initialState,
            IEnumerable<IState> states,
            IEnumerable<StateTransition<Type>> transitions
        ) : base(initialState, states.Select(it => new KeyValuePair<Type, IState>(it.GetType(), it)), transitions)
        {
        }
    }
}