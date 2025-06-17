using System;

namespace Modules.FSM
{
    
    public sealed class DecoratorState : IState
    {
        private readonly Action _onEnter;
        private readonly Action _onExit;
        private readonly Action<float> _onUpdate;

        private readonly IState _state;

        public DecoratorState(IState state,
            Action onEnter = null,
            Action onExit = null,
            Action<float> onUpdate = null
        )
        {
            _state = state ?? throw new ArgumentNullException(nameof(state));
            _onEnter = onEnter;
            _onExit = onExit;
            _onUpdate = onUpdate;
        }

        public void OnEnter()
        {
            _onEnter?.Invoke(); _state.OnEnter();
        }

        public void OnExit()
        {
            _onExit?.Invoke(); _state.OnExit();
        }

        public void OnUpdate(float deltaTime)
        {
            _onUpdate?.Invoke(deltaTime); _state.OnUpdate(deltaTime);
        }
    }
}