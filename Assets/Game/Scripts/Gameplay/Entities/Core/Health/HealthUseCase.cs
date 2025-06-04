using Leopotam.EcsLite.Di;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public readonly struct HealthUseCase
    {
        private readonly EcsPoolInject<Health> _healths;

        public bool Reduce(in int entity, in int range)
        {
            if (!_healths.Value.Has(entity))
            {
                return false;
            }
            if (!_healths.Value.Has(entity))
                return false;
            ref Health health = ref _healths.Value.Get(entity);
            if (health.current == 0)
                return false;
            health.current = math.max(0, health.current - range);
            return true;
        }

        public bool Exists(int entity)
        {
            return _healths.Value.Get(entity).current > 0;
        }
    }
}