using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public class AudioSourceInstaller : EcsViewInstaller
    {
        [SerializeField]
        private AudioSource _audioSource;
        
        public override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<AudioSourceView>().Add(entity).value = _audioSource;
        }
    }
}