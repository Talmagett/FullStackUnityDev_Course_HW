using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class LifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Lifetime>> _lifetimes;
        private readonly EcsEventInject<DestroyRequest> _requests;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _lifetimes.Value)
            {
                ref Lifetime lifetime = ref _lifetimes.Pools.Inc1.Get(entity);
                lifetime.value -= Time.deltaTime;
                if (lifetime.value <= 0)
                {
                    _requests.Value.Fire(new DestroyRequest{entity = entity});
                }
            }
        }
    }
}