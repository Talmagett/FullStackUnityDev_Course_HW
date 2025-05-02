// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class ProjectileIniitalizer : IEcsRunSystem
//     {
//         private readonly EcsFilterInject<Inc<ProjectileTag>, Exc<MoveEnabled>> _projectiles;
//         private readonly EcsPoolInject<MoveDirection> _moveDirections;
//         private readonly EcsPoolInject<MoveEnabled> _moveEnables;
//         private readonly EcsUseCaseInject<TransformUseCase> _transformUseCase;
//
//         public void Run(IEcsSystems systems)
//         {
//             foreach (int entity in _projectiles.Value)
//             {
//                 ref MoveDirection moveDirection = ref _moveDirections.Value.Get(entity);
//                 moveDirection.value = _transformUseCase.Value.GetForward(entity);
//                 _moveEnables.Value.Add(entity);
//             }
//         }
//     }
// }