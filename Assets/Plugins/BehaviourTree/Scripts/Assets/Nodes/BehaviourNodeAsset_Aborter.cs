using System;
using UnityEngine;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Aborter<TContext> : BehaviourNodeAsset<TContext>
    {
        [SerializeField]
        private BehaviourNodeAsset<TContext> _origin;

        [SerializeReference]
        private IBTConditionAsset _condition = default;

        [SerializeReference]
        private IBTActionAsset _onAbort = default;

        public override IBehaviourNode Create(TContext context)
        {
            return new BehaviourNodeAborter(
                _origin.Create(context),
                _condition?.Create(context),
                _onAbort?.Create(context)
            );
        }
    }
}