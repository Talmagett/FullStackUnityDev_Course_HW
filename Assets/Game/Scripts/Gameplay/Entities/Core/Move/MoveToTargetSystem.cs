using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveToTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveableTag, Target>> _filter;
        
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<UnitDirection> _unitDirections;
        private readonly EcsPoolInject<Target> _targets;
        
        private readonly EcsPoolInject<MoveDirection> _moveDirections;
        private readonly EcsPoolInject<RotateDirection> _rotateDirections;
        
        private readonly EcsPoolInject<AttackDistance> _attackDistances;
        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter.Value)
            {
                ref UnitDirection unitDirection = ref _unitDirections.Value.Get(entity);
                ref Target target = ref _targets.Value.Get(entity);
                ref Position position = ref _positions.Value.Get(entity);
                ref AttackDistance attackDistance = ref _attackDistances.Value.Get(entity);
                ref MoveDirection moveDirection = ref _moveDirections.Value.Get(entity);
                ref RotateDirection rotateDirection = ref _rotateDirections.Value.Get(entity);

                if (target.value == -1)
                {
                    moveDirection.value = float3.zero;
                    continue;
                }
                ref Position targetPosition = ref _positions.Value.Get(target.value);
                unitDirection.value = math.normalize(targetPosition.value - position.value);

                if(math.distancesq(targetPosition.value,position.value)<
                   attackDistance.value * attackDistance.value)
                {
                    moveDirection.value = float3.zero;
                    rotateDirection.value = unitDirection.value;
                    continue;
                }
                
                moveDirection.value = unitDirection.value;
                rotateDirection.value = unitDirection.value;
            }
        }
    }
}