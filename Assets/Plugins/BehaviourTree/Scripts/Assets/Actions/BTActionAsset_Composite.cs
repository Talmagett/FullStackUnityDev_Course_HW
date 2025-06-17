using System;
using Modules.BehaviourTree;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules
{
    [Serializable]
    public sealed class BTActionAsset_Composite : IBTActionAsset
    {
        [SerializeReference, HideLabel]
        private IBTActionAsset[] actions = default;

        public Action Create(object context)
        {
            int count = this.actions.Length;
            Action[] actions = new Action[count];

            for (int i = 0; i < count; i++)
                actions[i] = this.actions[i].Create(context);

            return () =>
            {
                for (int i = 0; i < count; i++)
                    actions[i].Invoke();
            };
        }
    }
}