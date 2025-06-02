using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public class AttackCooldownSystem: IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackCooldown>> _filter;

        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            var pool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                ref AttackCooldown attackCooldown = ref pool.Get(entity);
                if (attackCooldown.current <= 0) continue;
                
                attackCooldown.current -= deltaTime;
            }
        }
    }
}