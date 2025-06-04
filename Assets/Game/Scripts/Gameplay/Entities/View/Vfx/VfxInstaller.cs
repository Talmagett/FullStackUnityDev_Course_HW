using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public class VfxInstaller : EcsViewInstaller
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        public override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<VfxView>().Add(entity).value = _particleSystem;
        }
    }
}