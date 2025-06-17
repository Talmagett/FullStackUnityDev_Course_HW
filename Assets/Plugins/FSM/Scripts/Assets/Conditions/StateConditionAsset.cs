using System;

namespace Modules.FSM
{
    public interface IStateConditionAsset
    {
        Func<bool> Create(object context);
    }
}