using System;
using UnityEngine;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset<TContext> : MonoBehaviour
    {
        public abstract IBehaviourNode Create(TContext context);
    }
}