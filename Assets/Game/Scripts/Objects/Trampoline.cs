using UnityEngine;

namespace Game.Objects
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField] private float pushPower;
        [SerializeField] private AudioSource trampolineSource;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Rigidbody2D otherRigidbody2D)) return;
            otherRigidbody2D.AddForce(Vector2.up*pushPower, ForceMode2D.Impulse);
            trampolineSource.Play();
        }
    }
}