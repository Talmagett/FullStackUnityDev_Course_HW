using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RotatableTag>> _rotateables;
        private readonly EcsPoolInject<Rotation> _rotations;
        private readonly EcsPoolInject<RotationSpeed> _rotationSpeeds;
        private readonly EcsPoolInject<RotateDirection> _rotationDirections;

        public void Run(IEcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            foreach (int entity in _rotateables.Value)
            {
                ref Rotation rotation = ref _rotations.Value.Get(entity);
                ref RotationSpeed speed = ref _rotationSpeeds.Value.Get(entity);
                ref RotateDirection direction = ref _rotationDirections.Value.Get(entity);
                RotateUseCase.RotateStep(ref rotation, in direction, in speed, in deltaTime);
            }
        }
    }
}