using Leopotam.EcsLite.Di;
using Unity.Burst;
using Unity.Mathematics;

namespace SampleGame
{
    [BurstCompile]
    public readonly struct MoveUseCase
    {
        private readonly EcsPoolInject<MoveDirection> _moveDirections;

        public bool IsMoving(in int entity)
        {
            return math.any(_moveDirections.Value.Get(entity).value != float3.zero);
        }

        [BurstCompile]
        public static void MoveStep(
            ref Position position,
            in MoveDirection moveDirection,
            in MoveSpeed moveSpeed,
            in float deltaTime
        )
        {
            position.value += moveDirection.value * moveSpeed.value * deltaTime;
        }
    }
}