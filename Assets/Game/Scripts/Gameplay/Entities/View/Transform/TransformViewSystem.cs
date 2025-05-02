using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class TransformViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position, Rotation>> _entities;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _entities.Value)
            {
                ref TransformView transform = ref _entities.Pools.Inc1.Get(entity);
                ref Position position = ref _entities.Pools.Inc2.Get(entity);
                ref Rotation rotation = ref _entities.Pools.Inc3.Get(entity);
                transform.value.SetPositionAndRotation(position.value, rotation.value);
            }
        }
    }
}