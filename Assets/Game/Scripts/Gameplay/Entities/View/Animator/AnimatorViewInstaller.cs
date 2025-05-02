using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class AnimatorViewInstaller : EcsViewInstaller
    {
        [SerializeField]
        private Animator _animator;
        
        public override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<AnimatorView>().Add(entity).value = _animator;
        }
    }
}