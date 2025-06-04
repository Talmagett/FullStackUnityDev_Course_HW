using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public readonly struct TakeDamageUseCase
    {
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<Damage> _damages;
        private readonly EcsUseCaseInject<HealthUseCase> _healthUseCase;
        private readonly EcsEventInject<TakeDamageEvent> _takeDamageEvents;

        public bool TakeDamage(EcsPackedEntity source, EcsPackedEntity target)
        {
            if (!source.Unpack(_world.Value, out int sourceId) ||
                !target.Unpack(_world.Value, out int targetId))
                return false;

            ref int damage = ref _damages.Value.Get(sourceId).value;

            if (!_healthUseCase.Value.Reduce(targetId, damage))
                return false;
            _takeDamageEvents.Value.Fire(new TakeDamageEvent
            {
                source = source,
                target = target,
                damage = damage
            });

            return true;
        }
    }
}