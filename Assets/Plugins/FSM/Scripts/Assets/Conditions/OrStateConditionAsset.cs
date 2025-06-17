using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.FSM
{
    [Serializable, InlineProperty, LabelWidth(1)]
    public sealed class OrStateConditionAsset : IStateConditionAsset
    {
        [SerializeReference, HideLabel]
        private IStateConditionAsset[] conditions = default;

        public Func<bool> Create(object context)
        {
            int count = this.conditions.Length;
            Func<bool>[] result = new Func<bool>[count];

            for (int i = 0; i < count; i++)
                result[i] = this.conditions[i].Create(context);

            return () =>
            {
                for (int i = 0; i < count; i++)
                    if (result[i].Invoke())
                        return true;

                return false;
            };
        }
    }
}