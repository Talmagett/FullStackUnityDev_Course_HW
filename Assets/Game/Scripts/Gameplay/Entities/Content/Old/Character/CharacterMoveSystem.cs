using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class CharacterMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CharacterTag>> _characters;
        private readonly EcsPoolInject<UnitDirection> _unitDirections;
        private readonly EcsPoolInject<MoveDirection> _moveDirections;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _characters.Value)
            {
                ref UnitDirection unitDirection = ref _unitDirections.Value.Get(entity);
                _moveDirections.Value.Get(entity).value = unitDirection.value;
            }
        }
    }
}