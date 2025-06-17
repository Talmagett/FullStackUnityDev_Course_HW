using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// ReSharper disable UnusedParameter.Global

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Composite<TСontext> : BehaviourNodeAsset<TСontext>
    {
        [SerializeField]
        private BehaviourNodeAsset<TСontext>[] _children;

        public sealed override IBehaviourNode Create(TСontext context) => 
            this.Create(context, _children.Select(it => it.Create(context)));

        protected abstract IBehaviourNode Create(TСontext context, IEnumerable<IBehaviourNode> children);
    }
}