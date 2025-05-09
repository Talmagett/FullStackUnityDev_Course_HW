using Atomic.Entities;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class PistolVisualInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private ParticleSystem _vfx;

        [SerializeField]
        private AudioSource _audioSource;
        
        public override void Install(IEntity entity)
        {            
            entity.GetFireEvent().OnEvent += () =>
            {
                _vfx.Play();
                _audioSource.pitch = Random.Range(0.9f, 1.1f);
                _audioSource.Play();
            };
        }
    }
}