using DG.Tweening;
using Game.Components;
using UnityEngine;

namespace Game.Objects.Visual
{
    public class CharacterVisual : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private PushComponent pushComponent;
        [SerializeField] private PushComponent tossComponent;
        
        [Space]
        [SerializeField] private Animator animator;

        [SerializeField] private ParticleSystem pushParticle;
        [SerializeField] private ParticleSystem tossParticle;
        
        [Space]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip takeDamageClip;
        [SerializeField] private AudioClip pushClip;
        [SerializeField] private AudioClip tossClip;
        
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        private void OnEnable()
        {
            jumpComponent.OnJump += OnJump;
            pushComponent.OnPush += OnPush;
            tossComponent.OnPush += OnToss;
            healthComponent.OnTakeDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            jumpComponent.OnJump -= OnJump;
            pushComponent.OnPush -= OnPush;
            tossComponent.OnPush -= OnToss;
            healthComponent.OnTakeDamage -= OnTakeDamage;
        }

        private void OnPush()
        {
            pushParticle.Play();            
            audioSource.PlayOneShot(pushClip);
        }

        private void OnToss()
        {
            tossParticle.Play();            
            audioSource.PlayOneShot(tossClip);
        }

        private void OnTakeDamage()
        {
            audioSource.PlayOneShot(takeDamageClip);
            animator.SetTrigger(TakeDamage);
        }

        private void OnJump()
        {
            animator.SetTrigger(Jump);
            audioSource.PlayOneShot(jumpClip);
        }
    }
}