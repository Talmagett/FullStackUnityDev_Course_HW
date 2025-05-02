// using Leopotam.EcsLite.Di;
//
// namespace SampleGame
// {
//     public readonly struct AmmoUseCases
//     {
//         private readonly EcsPoolInject<Ammo> _ammos;
//
//         public bool Exists(in int entity)
//         {
//             return _ammos.Value.Get(entity).current > 0;
//         }
//         
//         public void Spend(in int entity)
//         {
//             ref Ammo ammo = ref _ammos.Value.Get(entity);
//             ammo.current--;
//         }
//     }
// }