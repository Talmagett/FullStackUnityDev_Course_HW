using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class PlayerFireController : IEcsRunSystem
    {
        private readonly EcsSingletonInject<InputData> _inputData;
        private readonly EcsFilterInject<Inc<UnitFireRequired>> _units;
        private readonly EcsUseCaseInject<TeamUseCase> _teamUseCase;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            bool isFire = _inputData.Value.isFire;
            
            foreach (int entity in _units.Value)
            {
                if (!_teamUseCase.Value.IsTeam(entity, TeamType.BLUE))
                    continue;

                ref UnitFireRequired required = ref _units.Pools.Inc1.Get(entity);
                required.value = isFire;
            }
        }
    }
}