using Leopotam.EcsLite.Di;
using Unity.Mathematics;

namespace SampleGame
{
    public readonly struct TransformUseCase
    {
        private readonly EcsPoolInject<Position> _positions;
        private readonly EcsPoolInject<Rotation> _rotations;

        public float3 GetForward(in int entity)
        {
            ref quaternion quaternion = ref _rotations.Value.Get(entity).value;
            return math.mul(quaternion, new float3(0, 0, 1));
        }
    }
}