using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveAnimSystem : IEcsRunSystem
    {
        private static readonly int IsMoving = Animator.StringToHash(nameof(IsMoving));
        
        private readonly EcsFilterInject<Inc<MoveableTag, AnimatorView>> _moveables;
        private readonly EcsUseCaseInject<MoveUseCase> _moveUseCase;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _moveables.Value)
            {
                ref AnimatorView animator = ref _moveables.Pools.Inc2.Get(entity);
                bool isMoving = _moveUseCase.Value.IsMoving(entity);
                animator.value.SetBool(IsMoving, isMoving);
            }
        }
    }
}