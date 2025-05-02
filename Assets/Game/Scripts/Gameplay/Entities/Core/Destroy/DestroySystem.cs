using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public sealed class DestroySystem : IEcsRunSystem
    {
        private readonly EcsEventInject<DestroyRequest> _requests;
        private readonly EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            while (_requests.Value.Consume(out DestroyRequest request)) 
                _world.Value.DelEntity(request.entity);
        }
    }
}