using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace SampleGame
{
    public sealed class PlayerMoveController : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitDirection>> _units;
        private readonly EcsSingletonInject<InputData> _inputData;
        private readonly EcsPoolInject<TeamType> _teamTypes;
        
        public void Run(IEcsSystems systems)
        {
            ref float3 moveDirection = ref _inputData.Value.moveDirection;

            foreach (int entity in _units.Value)
                if (_teamTypes.Value.Get(entity) == TeamType.BLUE)
                    _units.Pools.Inc1.Get(entity).value = moveDirection;
        }
    }
}