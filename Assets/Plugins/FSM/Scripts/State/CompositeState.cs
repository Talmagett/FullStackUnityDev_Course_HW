using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

// ReSharper disable ForCanBeConvertedToForeach

namespace Modules.FSM
{
    public sealed class CompositeState : IState
    {
        [ShowInInspector, ReadOnly]
        private readonly List<IState> _states;

        public CompositeState() =>
            _states = new List<IState>();

        public CompositeState(params IState[] states) =>
            _states = new List<IState>(states);

        public CompositeState(IEnumerable<IState> states) =>
            _states = new List<IState>(states);

        public void AddState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            _states.Add(state);
        }

        public bool RemoveState(IState state)
        {
            return state != null && _states.Remove(state);
        }

        public void OnEnter()
        {
            for (int i = 0; i < _states.Count; i++)
                _states[i].OnEnter();
        }

        public void OnExit()
        {
            for (int i = 0; i < _states.Count; i++)
                _states[i].OnExit();
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _states.Count; i++)
                _states[i].OnUpdate(deltaTime);
        }
    }
}