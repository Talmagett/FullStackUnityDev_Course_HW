using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathableTag>> _deathables;
        private readonly EcsPoolInject<Health> _healths;
        private readonly EcsEventInject<DestroyRequest> _destroyRequest;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _deathables.Value)
            {
                Health health = _healths.Value.Get(entity);
                if (health.current == 0) 
                    _destroyRequest.Value.Fire(new DestroyRequest() {entity = entity});
            }
        }
    }
}