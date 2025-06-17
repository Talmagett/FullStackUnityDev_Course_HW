using System;

namespace Modules.FSM
{
    public sealed class BaseState : IState
    {
        private readonly Action _onEnter;
        private readonly Action _onExit;
        private readonly Action<float> _onUpdate;
        
        public BaseState(Action onEnter = null, Action onExit = null, Action<float> onUpdate = null)
        {
            _onEnter = onEnter;
            _onExit = onExit;
            _onUpdate = onUpdate;
        }

        public void OnEnter() => _onEnter?.Invoke();

        public void OnExit() => _onExit?.Invoke();

        public void OnUpdate(float deltaTime) => _onUpdate?.Invoke(deltaTime);
    }
}