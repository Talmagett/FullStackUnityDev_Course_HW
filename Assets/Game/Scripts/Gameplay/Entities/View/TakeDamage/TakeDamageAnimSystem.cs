using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class TakeDamageAnimSystem : IEcsRunSystem
    {
        private static readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));

        private readonly EcsEventInject<TakeDamageEvent> _events;
        private readonly EcsPoolInject<AnimatorView> _animators;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (TakeDamageEvent damageEvent in _events.Value)
            {
                if (!damageEvent.target.Unpack(_world.Value, out int target))
                    continue;

                if (!_animators.Value.Has(target)) 
                    continue;

                Animator animator = _animators.Value.Get(target).value;
                animator.SetTrigger(TakeDamage);
            }
        }
    }
}