using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class TransformViewInstaller : EcsViewInstaller
    {
        [SerializeField]
        private Transform _transform;
        
        public override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<TransformView>().Add(entity).value = _transform;
        }
    }
}