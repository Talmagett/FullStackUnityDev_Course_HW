using System.Collections.Generic;
using PlasticPipe.PlasticProtocol.Client;

namespace Modules.BehaviourTree
{
    public sealed class BehaviourNodeSequence : BehaviourNodeComposite
    {
        private int _nodeIndex;

        public BehaviourNodeSequence(IEnumerable<IBehaviourNode> nodes) : base(nodes)
        {
        }
        
        public BehaviourNodeSequence(params IBehaviourNode[] nodes) : base(nodes)
        {
        }

        protected override void OnEnable()
        {
            _nodeIndex = 0;
        }
        
        protected override void OnAbort()
        {
            if (_nodes.Count == 0)
                return;

            IBehaviourNode currentNode = _nodes[_nodeIndex];
            currentNode.Abort();
        }
        
        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            Start:
            if (_nodeIndex == _nodes.Count)
                return BehaviourResult.SUCCESS;
            
            IBehaviourNode currentNode = _nodes[_nodeIndex];
            BehaviourResult result = currentNode.Run(deltaTime);

            if (result != BehaviourResult.SUCCESS)
                return result;

            _nodeIndex++;
            goto Start;
        }
    }
}