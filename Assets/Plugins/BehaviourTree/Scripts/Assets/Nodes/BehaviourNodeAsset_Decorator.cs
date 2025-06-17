using System;
using UnityEngine;

namespace Modules.BehaviourTree
{
    [Serializable]
    public abstract class BehaviourNodeAsset_Decorator<TContext> : BehaviourNodeAsset<TContext>
    {
        [SerializeField]
        private BehaviourNodeAsset<TContext> _origin;

        [SerializeField]
        private BehaviourNodeDecorator.OverrideResult _overrideResult = BehaviourNodeDecorator.OverrideResult.None;

        [Space]
        [SerializeReference]
        private IBTActionAsset _onEnable = default;

        [SerializeReference]
        private IBTActionAsset _onDisable = default;
        
        [SerializeReference]
        private IBTActionAsset_Float _onUpdate = default;

        [SerializeReference]
        private IBTActionAsset _onAbort = default;

        public override IBehaviourNode Create(TContext context)
        {
            return new BehaviourNodeDecorator(
                _origin.Create(context),
                _overrideResult,
                _onEnable?.Create(context),
                _onDisable?.Create(context),
                _onUpdate?.Create(context),
                _onAbort?.Create(context)
            );
        }
    }
}