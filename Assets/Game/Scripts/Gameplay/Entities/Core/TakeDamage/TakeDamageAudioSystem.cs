using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public class TakeDamageAudioSystem : IEcsRunSystem
    {
        private readonly EcsEventInject<TakeDamageEvent> _events;
        private readonly EcsWorldInject _world;
        private readonly EcsPoolInject<AudioSourceView> _audioSourcePool;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (TakeDamageEvent damageEvent in _events.Value)
            {
                if (!damageEvent.target.Unpack(_world.Value, out int target))
                    continue;
                if(_audioSourcePool.Value.Has(target))
                    _audioSourcePool.Value.Get(target).value.Play();
            }
        }
    }
}