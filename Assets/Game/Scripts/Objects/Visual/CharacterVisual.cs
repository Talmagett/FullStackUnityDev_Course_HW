using Game.Components;
using UnityEngine;

namespace Game.Objects.Visual
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private PushComponent pushComponent;
        
        [Space]
        [SerializeField] private ParticleSystem pushParticle;

        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip pushClip;
        [SerializeField] private AudioClip tossClip;
        
        private void OnEnable()
        {
            jumpComponent.OnJump += OnJump;
            pushComponent.OnPush += OnPush;
            healthComponent.OnTakeDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            jumpComponent.OnJump -= OnJump;
            pushComponent.OnPush -= OnPush;
            healthComponent.OnTakeDamage -= OnTakeDamage;
        }

        private void OnPush()
        {
            pushParticle.Play();            
            audioSource.PlayOneShot(pushClip);
        }

        private void OnTakeDamage()
        {
            
        }

        private void OnJump()
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }
}