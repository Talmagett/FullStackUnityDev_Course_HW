using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class FireAnimSystem : IEcsRunSystem
    {
        private static readonly int Fire = Animator.StringToHash(nameof(Fire));

        private readonly EcsEventInject<FireEvent> _events;
        private readonly EcsPoolInject<AnimatorView> _animators;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (FireEvent fireEvent in _events.Value)
            {
                if (!fireEvent.entity.Unpack(_world.Value, out int entity))
                    continue;

                if (!_animators.Value.Has(entity))
                    continue;

                Animator animator = _animators.Value.Get(entity).value;
                animator.SetTrigger(Fire);
            }
        }
    }
}