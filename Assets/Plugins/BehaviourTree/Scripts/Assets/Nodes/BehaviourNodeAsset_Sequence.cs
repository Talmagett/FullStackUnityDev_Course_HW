using System;
using System.Collections.Generic;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Sequence<TSource> : BehaviourNodeAsset_Composite<TSource>
    {
        protected override IBehaviourNode Create(TSource context, IEnumerable<IBehaviourNode> children) => 
            new BehaviourNodeSequence(children);
    }
}