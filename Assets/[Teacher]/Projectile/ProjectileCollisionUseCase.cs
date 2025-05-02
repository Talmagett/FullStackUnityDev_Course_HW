// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
//
// namespace SampleGame
// {
//     public readonly struct ProjectileCollisionUseCase
//     {
//         private readonly EcsWorldInject _world;
//         private readonly EcsPoolInject<Damage> _damages;
//         private readonly EcsUseCaseInject<TakeDamageUseCase> _takeDamageUseCase;
//         private readonly EcsEventInject<DespawnRequest> _despawnRequests;
//
//         public bool Collide(in EcsPackedEntity projectile, in EcsPackedEntity target)
//         {
//             if (!projectile.Unpack(_world.Value, out int projectileId) || 
//                 !target.Unpack(_world.Value, out int targetId))
//                 return false;
//
//             return this.Collide(projectileId, targetId);
//         }
//         
//         public bool Collide(in int projectile, in int target)
//         {
//             ref Damage damage = ref _damages.Value.Get(projectile);
//             EcsPackedEntity source = _world.Value.PackEntity(projectile);
//
//             if (!_takeDamageUseCase.Value.TakeDamage(target, damage.value, source)) 
//                 return false;
//             
//             _despawnRequests.Value.Fire(new DespawnRequest{ entity = projectile});
//             return true;
//         }
//     }
// }