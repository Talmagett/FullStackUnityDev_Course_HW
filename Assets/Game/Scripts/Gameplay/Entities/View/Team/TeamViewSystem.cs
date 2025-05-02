using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class TeamViewSystem : IEcsRunSystem
    {
        private readonly TeamViewConfig _teamConfig;
        private readonly EcsFilterInject<Inc<TeamView, TeamType>> _views;

        public TeamViewSystem(TeamViewConfig teamConfig)
        {
            _teamConfig = teamConfig;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entitiy in _views.Value)
            {
                ref Renderer[] renderers = ref _views.Pools.Inc1.Get(entitiy).renderers;
                ref TeamType teamType = ref _views.Pools.Inc2.Get(entitiy);
                TeamViewUseCase.SetTeam(in renderers, in teamType, in _teamConfig);
            }
        }
    }
}