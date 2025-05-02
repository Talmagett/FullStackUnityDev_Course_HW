using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class TeamTypeInstaller : EcsComponentInstaller<TeamType>
    {
        [SerializeField]
        private TeamType _teamType;
        
        protected override TeamType GetValue()
        {
            return _teamType;
        }
    }
}