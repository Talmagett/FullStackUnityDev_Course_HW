using System;

namespace Modules.BehaviourTree
{
    public class BehaviourNodeDecorator : BehaviourNode
    {
        public enum OverrideResult
        {
            None = 0,
            Success = 1,
            Failure = 2,
            Running = 3
        }

        private readonly IBehaviourNode _origin;
        private readonly OverrideResult _overrideResult;

        private readonly Action _onEnable;
        private readonly Action _onDisable;
        private readonly Action<float> _onUpdate;
        private readonly Action _onAbort;

        public BehaviourNodeDecorator(
            IBehaviourNode origin,
            OverrideResult overrideResult = OverrideResult.None,
            Action onEnable = null,
            Action onDisable = null,
            Action<float> onUpdate = null,
            Action onAbort = null
        )
        {
            _origin = origin ?? throw new ArgumentNullException(nameof(origin));
            _overrideResult = overrideResult;
            
            _onEnable = onEnable;
            _onDisable = onDisable;
            _onUpdate = onUpdate;
            _onAbort = onAbort;
        }

        protected override void OnEnable() => _onEnable?.Invoke();

        protected override void OnDisable() => _onDisable?.Invoke();

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            _onUpdate?.Invoke(deltaTime);
            BehaviourResult result = _origin.Run(deltaTime);
            
            return _overrideResult switch
            {
                OverrideResult.None => result,
                OverrideResult.Success => BehaviourResult.SUCCESS,
                OverrideResult.Failure => BehaviourResult.FAILURE,
                OverrideResult.Running => BehaviourResult.RUNNING,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override void OnAbort()
        {
            _onAbort?.Invoke();
            _origin.Abort();
        }
    }
}