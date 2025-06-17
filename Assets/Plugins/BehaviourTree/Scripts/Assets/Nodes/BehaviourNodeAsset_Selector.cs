using System;
using System.Collections.Generic;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Selector<TContext> : BehaviourNodeAsset_Composite<TContext>
    {
        protected override IBehaviourNode Create(TContext context, IEnumerable<IBehaviourNode> children) => 
            new BehaviourNodeSelector(children);
    }
}