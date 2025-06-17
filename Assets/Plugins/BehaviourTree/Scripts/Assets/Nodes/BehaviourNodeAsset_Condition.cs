using System;
using UnityEngine;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Condition<TContext> : BehaviourNodeAsset<TContext>
    {
        [SerializeReference]
        private IBTConditionAsset _condition = default;

        public override IBehaviourNode Create(TContext context) =>
            new BehaviourNodeCondition(_condition?.Create(context));
    }
}