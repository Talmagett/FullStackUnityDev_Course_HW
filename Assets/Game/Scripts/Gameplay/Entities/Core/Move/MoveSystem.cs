using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveableTag>> _filter;
        
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<MoveDirection> _moveDirections;
        private readonly EcsPoolInject<MoveSpeed> _moveSpeeds;

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            foreach (int entity in _filter.Value)
            {
                ref Position position = ref _positions.Value.Get(entity);
                ref MoveDirection moveDirection = ref _moveDirections.Value.Get(entity);
                ref MoveSpeed moveSpeed = ref _moveSpeeds.Value.Get(entity);
                MoveUseCase.MoveStep(ref position, in moveDirection, in moveSpeed, in deltaTime);
            }
        }
    }
}