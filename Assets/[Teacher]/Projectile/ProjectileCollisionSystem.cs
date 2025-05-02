// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
//
// namespace SampleGame
// {
//     public sealed class ProjectileCollisionSystem : IEcsRunSystem
//     {
//         private readonly EcsEventInject<ProjectileCollisionRequest> _requests;
//         private readonly EcsUseCaseInject<ProjectileCollisionUseCase> _useCase;
//         
//         public void Run(IEcsSystems systems)
//         {
//             while (_requests.Value.Consume(out ProjectileCollisionRequest request))
//                 _useCase.Value.Collide(request.projectile, request.target);
//         }
//     }
// }