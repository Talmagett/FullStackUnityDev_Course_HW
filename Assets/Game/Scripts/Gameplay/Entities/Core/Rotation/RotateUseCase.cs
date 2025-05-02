using Leopotam.EcsLite.Di;
using Unity.Burst;
using Unity.Mathematics;

namespace SampleGame
{
    [BurstCompile]
    public readonly struct RotateUseCase
    {
        private static readonly float3 UP = new(0, 1, 0);

        [BurstCompile]
        public static void RotateStep(
            ref Rotation rotation,
            in RotateDirection direction,
            in RotationSpeed speed,
            in float deltaTime
        )
        {
            if (math.all(direction.value == float3.zero))
                return;

            quaternion target = quaternion.LookRotation(direction.value, UP);
            rotation.value = math.slerp(rotation.value, target, speed.value * deltaTime);
        }
    }
}