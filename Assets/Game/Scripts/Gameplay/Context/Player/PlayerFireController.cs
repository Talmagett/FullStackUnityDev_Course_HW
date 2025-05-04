using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class PlayerFireController : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BaseTag>> _bases;
        private readonly EcsFilterInject<Inc<UnitSpawnRequired>> _units;
        private readonly EcsUseCaseInject<TeamUseCase> _teamUseCase;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            if(Input.GetKeyDown(KeyCode.Space))            
            foreach (int entity in _bases.Value)
            {
                if (!_teamUseCase.Value.IsTeam(entity, TeamType.BLUE))
                    continue;

                ref UnitSpawnRequired required = ref _units.Pools.Inc1.Get(entity);
                required.value=true;
                required.type = UnitSpawnType.Archer;
            }
        }
    }
}