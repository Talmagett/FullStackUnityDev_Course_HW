using System;

namespace Modules.BehaviourTree
{
    public class BehaviourNodeAction : BehaviourNode
    {
        private readonly Action _action;

        public BehaviourNodeAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            _action.Invoke();
            return BehaviourResult.SUCCESS;
        }
    }
}