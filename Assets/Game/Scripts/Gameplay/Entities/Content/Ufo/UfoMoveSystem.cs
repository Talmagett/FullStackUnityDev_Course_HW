using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{

    public sealed class UfoMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UfoTag>> _characters;
        private readonly EcsPoolInject<UnitDirection> _unitDirections;
        private readonly EcsPoolInject<MoveDirection> _moveDirections;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _characters.Value)
            {
                ref UnitDirection unitDirection = ref _unitDirections.Value.Get(entity);
                ref MoveDirection moveDirection = ref _moveDirections.Value.Get(entity);
                moveDirection.value = unitDirection.value;
            }
        }
    }
}