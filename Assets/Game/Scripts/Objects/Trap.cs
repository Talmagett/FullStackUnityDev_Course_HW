using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private int damage=1;
        
        private void OnEnable()
        {
            healthComponent.OnDead += OnHealthEmpty;
        }

        private void OnDisable()
        {
            healthComponent.OnDead -= OnHealthEmpty;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.TryGetComponent(out IDamageable damageable))
                return;
            damageable.TakeDamage(damage);
            OnHealthEmpty();
        }

        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }
    }
}