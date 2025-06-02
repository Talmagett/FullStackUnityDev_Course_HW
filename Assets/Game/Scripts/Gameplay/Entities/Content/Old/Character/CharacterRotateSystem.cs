using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class CharacterRotateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CharacterTag>> _characters;
        private readonly EcsPoolInject<UnitDirection> _unitDirections;
        private readonly EcsPoolInject<RotateDirection> _rotateDirections;
        
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _characters.Value)
            {
                ref UnitDirection unitDirection = ref _unitDirections.Value.Get(entity);
                _rotateDirections.Value.Get(entity).value =  unitDirection.value;
            }
        }
    }
}