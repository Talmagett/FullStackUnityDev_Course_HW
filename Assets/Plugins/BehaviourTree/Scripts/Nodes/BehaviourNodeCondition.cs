using System;

namespace Modules.BehaviourTree
{
    public class BehaviourNodeCondition : BehaviourNode
    {
        private readonly Func<bool> _condition;

        public BehaviourNodeCondition(Func<bool> condition)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            return _condition.Invoke() ? BehaviourResult.SUCCESS : BehaviourResult.FAILURE;
        }
    }
}