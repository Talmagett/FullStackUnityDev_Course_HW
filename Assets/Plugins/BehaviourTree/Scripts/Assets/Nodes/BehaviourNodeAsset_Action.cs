using System;
using UnityEngine;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Action<TContext> : BehaviourNodeAsset<TContext>
    {
        [SerializeReference]
        private IBTActionAsset _action = default;

        public override IBehaviourNode Create(TContext context) => 
            new BehaviourNodeAction(_action?.Create(context));
    }
}