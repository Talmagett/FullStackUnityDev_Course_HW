// TODO: Uncomment me
using Atomic.Elements;
using Atomic.Entities;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class DeathSoundBehaviour : IEntityInit, IEntityDispose
    {
        private readonly AudioClip[] _clips;

        public DeathSoundBehaviour(AudioClip[] clips)
        {
            _clips = clips;
        }
        
        private AudioSource _audioSource;
        private IEvent _deathEvent;

        public void Init(in IEntity entity)
        {
            _audioSource = entity.GetAudioSource();
            _deathEvent = entity.GetDeathEvent();
            _deathEvent.Subscribe(this.OnDeath);
        }

        public void Dispose(in IEntity entity)
        {
            _deathEvent.Unsubscribe(this.OnDeath);
        }

        private void OnDeath()
        {
            if (_clips.Length == 0)
                return;

            int randomIndex = Random.Range(0, _clips.Length);
            AudioClip clip = _clips[randomIndex];
            _audioSource.PlayOneShot(clip);
        }
    }
}