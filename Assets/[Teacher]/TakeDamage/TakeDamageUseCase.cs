// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
//
// namespace SampleGame
// {
//     public readonly struct TakeDamageUseCase
//     {
//         private readonly EcsWorldInject _world;
//         private readonly EcsEventInject<TakeDamageEvent> _damageEvent;
//         private readonly EcsUseCaseInject<HealthUseCase> _healthUseCase;
//
//         public bool TakeDamage(in int entity, in int damage, in EcsPackedEntity source)
//         {
//             if (!_healthUseCase.Value.Reduce(in entity, in damage))
//                 return false;
//
//             _damageEvent.Value.Fire(new TakeDamageEvent
//             {
//                 target = _world.Value.PackEntity(entity),
//                 damage = damage,
//                 source = source
//             });
//
//             return true;
//         }
//
//         public bool TakeDamage(in EcsPackedEntity target, in int damage, in EcsPackedEntity source)
//         {
//             return target.Unpack(_world.Value, out int entity) && this.TakeDamage(entity, damage, source);
//         }
//     }
// }