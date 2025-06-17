using UnityEngine;

namespace Modules.FSM
{
    public abstract class StateAsset<TContext> : MonoBehaviour
    {
        public abstract IState CreateState(TContext context);
    }
}