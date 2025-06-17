using System;
using System.Collections.Generic;

namespace Modules.BehaviourTree
{
    public abstract class BehaviourNodeComposite : BehaviourNode
    {
        protected readonly List<IBehaviourNode> _nodes;

        protected BehaviourNodeComposite(IEnumerable<IBehaviourNode> nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException(nameof(nodes));

            _nodes = new List<IBehaviourNode>(nodes);
        }
    }
}