using System;

namespace Modules.BehaviourTree
{
    public interface IBTActionAsset_Float
    {
        Action<float> Create(object context);
    }
}