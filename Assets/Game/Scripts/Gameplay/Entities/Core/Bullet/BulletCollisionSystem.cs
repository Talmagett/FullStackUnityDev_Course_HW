using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class BulletCollisionSystem : IEcsRunSystem
    {
        private readonly EcsEventInject<BulletCollisionRequest> _collisionRequests;
        private readonly EcsEventInject<DestroyRequest> _destroyRequests;
        private readonly EcsUseCaseInject<TakeDamageUseCase> _takeDamageUseCase;

        public void Run(IEcsSystems systems)
        {
            foreach (BulletCollisionRequest request in _collisionRequests.Value)
                if (_takeDamageUseCase.Value.TakeDamage(request.bullet, request.target))
                    _destroyRequests.Value.Fire(new DestroyRequest {entity = request.bullet.Id});
        }
    }
}