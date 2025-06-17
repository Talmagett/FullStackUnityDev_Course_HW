using System;
using Modules.BehaviourTree;

namespace Modules.BehaviourTree
{
    public class BehaviourNodeAborter : BehaviourNode
    {
        private readonly IBehaviourNode _origin;
        private readonly Func<bool> _condition;
        private readonly Action _onAbort;
        
        private bool _lastState;
        
        public BehaviourNodeAborter(IBehaviourNode origin, Func<bool> condition, Action onAbort = null)
        {
            _origin = origin ?? throw new ArgumentNullException(nameof(origin));
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
            _onAbort = onAbort;
        }

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            this.TryAbort();
            return _origin.Run(deltaTime);
        }

        private void TryAbort()
        {
            bool currentState = _condition.Invoke();
            if (currentState == _lastState)
                return;

            _lastState = currentState;
            _origin.Abort();
            _onAbort?.Invoke();
        }
    }
}