using Atomic.Entities;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class ItemPickUpVisualInstaller : SceneEntityInstaller
    {
        [SerializeField]
        private ParticleSystem _vfx;

        [SerializeField]
        private GameObject _visual;

        [SerializeField]
        private AudioSource _audioSource;
        
        public override void Install(IEntity entity)
        {            
            entity.GetItemPickUpEvent().OnEvent += () =>
            {
                _vfx.Play();
                _audioSource.Play();
                _visual.SetActive(false);
            };
        }
    }
}