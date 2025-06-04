using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public class TakeDamageVfxSystem : IEcsRunSystem
    {
        private readonly EcsEventInject<TakeDamageEvent> _events;
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<VfxView> _vfxPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (TakeDamageEvent damageEvent in _events.Value)
            {
                if (!damageEvent.target.Unpack(_world.Value, out int target))
                    continue;

                if(_vfxPool.Value.Has(target))
                    _vfxPool.Value.Get(target).value.Play();
            }
        }
    }
}