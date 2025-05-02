// using System;
// using Leopotam.EcsLite.Di;
//
// namespace SampleGame
// {
//     public readonly struct HealthUseCase
//     {
//         private readonly EcsPoolInject<Health> _healths;
//
//         public bool Exists(in int entity)
//         {
//             return _healths.Value.Get(entity).current > 0;
//         }
//         
//         public bool Reduce(in int entity, in int range)
//         {
//             if (!_healths.Value.Has(entity))
//                 return false;
//
//             ref Health health = ref _healths.Value.Get(entity);
//             return Reduce(ref health, range);
//         }
//         
//         public static bool Reduce(ref Health health, in int range)
//         {
//             if (range < 0)
//                 throw new Exception($"Range can't be less than zero! Actual range {range}");
//
//             if (range == 0)
//                 return false;
//
//             if (!Exists(in health))
//                 return false;
//             
//             health.current = Math.Max(0, health.current - range);
//             
//             
//             
//             // this.OnStateChanged?.Invoke();
//             // this.OnHealthChanged?.Invoke(this.current);
//             //
//             // if (this.current == 0)
//             //     this.OnHealthEmpty?.Invoke();
//
//             return true;
//         }
//
//         public static bool Exists(in Health health)
//         {
//             return health.current > 0;
//         }
//     }
// }