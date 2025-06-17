using System.Collections.Generic;

namespace Modules.BehaviourTree
{
    public class BehaviourNodeSelector : BehaviourNodeComposite
    {
        private int _nodeIndex;

        public BehaviourNodeSelector(IEnumerable<IBehaviourNode> nodes) : base(nodes)
        {
        }
        
        public BehaviourNodeSelector(params IBehaviourNode[] nodes) : base(nodes)
        {
        }

        protected override void OnEnable()
        {
            _nodeIndex = 0;
        }

        protected override BehaviourResult OnUpdate(float deltaTime)
        {
            Start:
            if (_nodeIndex == _nodes.Count)
                return BehaviourResult.FAILURE;
            
            IBehaviourNode currentNode = _nodes[_nodeIndex];
            BehaviourResult result = currentNode.Run(deltaTime);

            if (result != BehaviourResult.FAILURE)
                return result;
            
            _nodeIndex++;
            goto Start;
        }

        protected override void OnAbort()
        {
            if (_nodes.Count == 0)
                return;
            
            IBehaviourNode currentNode = _nodes[_nodeIndex];
            currentNode.Abort();
        }
    }
}