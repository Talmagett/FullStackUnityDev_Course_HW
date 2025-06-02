using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class AttackAnimSystem : IEcsRunSystem
    {
        private static readonly int Attack = Animator.StringToHash(nameof(Attack));

        private readonly EcsEventInject<AttackEvent> _events;
        private readonly EcsPoolInject<AnimatorView> _animators;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var fireEvent in _events.Value)
            {
                if (!fireEvent.entity.Unpack(_world.Value, out int entity))
                    continue;

                if (!_animators.Value.Has(entity))
                    continue;

                Animator animator = _animators.Value.Get(entity).value;
                animator.SetTrigger(Attack);
            }
        }
    }
}