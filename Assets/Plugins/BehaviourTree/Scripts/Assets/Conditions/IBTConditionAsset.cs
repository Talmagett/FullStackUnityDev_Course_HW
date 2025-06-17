using System;

namespace Modules.BehaviourTree
{
    public interface IBTConditionAsset
    {
        Func<bool> Create(object context);
    }
}