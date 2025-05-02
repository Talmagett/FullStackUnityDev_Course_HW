// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class FireCooldownSystem : IEcsRunSystem
//     {
//         private readonly EcsFilterInject<Inc<FireCooldown>> _fireCooldowns;
//
//         public void Run(IEcsSystems systems)
//         {
//             foreach (int entity in _fireCooldowns.Value)
//             {
//                 float deltaTime = Time.deltaTime;
//                 ref FireCooldown cooldown = ref _fireCooldowns.Pools.Inc1.Get(entity);
//                 if (cooldown.current > 0)
//                     cooldown.current -= deltaTime;
//             }
//         }
//     }
// }