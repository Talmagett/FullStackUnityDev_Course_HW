using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Modules.FSM
{
    public class StateMachine<TKey> : IStateMachine<TKey>
    {
        protected static readonly EqualityComparer<TKey> s_keyComparer = EqualityComparer<TKey>.Default;

        public event Action<TKey> OnStateChanged;
        public event Action<TKey> OnStateAdded;
        public event Action<TKey> OnStateRemoved;

        [ShowInInspector, ReadOnly]
        public TKey CurrentState => _currentState.Key;
        
        public int StateCount => _states.Count;

        [ShowInInspector, ReadOnly]
        public IReadOnlyCollection<TKey> AllStates => _states.Keys;

        private readonly Dictionary<TKey, IState> _states;

        protected KeyValuePair<TKey, IState> _currentState;
        
        public StateMachine()
        {
            _states = new Dictionary<TKey, IState>();
            _currentState = new KeyValuePair<TKey, IState>();
        }

        public StateMachine(TKey initialState, params (TKey, IState)[] states)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));

            _states = states.ToDictionary(it => it.Item1, it => it.Item2);
            _currentState = new KeyValuePair<TKey, IState>(initialState, _states[initialState]);
        }

        public StateMachine(TKey initialState, IEnumerable<(TKey, IState)> states)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));

            _states = states.ToDictionary(it => it.Item1, it => it.Item2);
            _currentState = new KeyValuePair<TKey, IState>(initialState, _states[initialState]);
        }

        public StateMachine(TKey initialState, params KeyValuePair<TKey, IState>[] states)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));

            _states = new Dictionary<TKey, IState>(states);
            _currentState = new KeyValuePair<TKey, IState>(initialState, _states[initialState]);
        }

        public StateMachine(TKey initialState, IEnumerable<KeyValuePair<TKey, IState>> states)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));

            _states = new Dictionary<TKey, IState>(states);
            _currentState = new KeyValuePair<TKey, IState>(initialState, _states[initialState]);
        }

        [Button]
        public bool TryChangeState(TKey key, Action transition = null)
        {
            if (s_keyComparer.Equals(_currentState.Key, key))
                return false;

            this.ChangeState(key, transition);
            return true;
        }

        [Button]
        public void ChangeState(TKey key, Action transition = null)
        {
            _currentState.Value?.OnExit();
            transition?.Invoke();

            _currentState = new KeyValuePair<TKey, IState>(key, _states[key]);
            _currentState.Value?.OnEnter();

            this.OnStateChanged?.Invoke(key);
        }

        public void AddState(TKey key, IState state)
        {
            _states.Add(key, state);
            this.OnStateAdded?.Invoke(key);
        }

        public bool RemoveState(TKey key)
        {
            if (!_states.Remove(key))
                return false;

            this.OnStateRemoved?.Invoke(key);
            return true;
        }

        public bool ContainsState(TKey key)
        {
            return _states.ContainsKey(key);
        }

        public virtual void OnEnter()
        {
            _currentState.Value?.OnEnter();
        }

        public virtual void OnUpdate(float deltaTime)
        {
            _currentState.Value?.OnUpdate(deltaTime);
        }

        public virtual void OnExit()
        {
            _currentState.Value?.OnExit();
        }
    }
}