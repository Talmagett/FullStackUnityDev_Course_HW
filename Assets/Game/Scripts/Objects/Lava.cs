using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    public class Lava : MonoBehaviour
    {
        [SerializeField] private AudioSource lavaAudioSource;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out IDamageable damageable))
                return;
            damageable.TakeDamage(99999);
            lavaAudioSource.Play();
            //Destroy(other.gameObject);
        }
    }
}