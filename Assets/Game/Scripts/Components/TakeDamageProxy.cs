using UnityEngine;

namespace Game.Components
{
    public class TakeDamageProxy : MonoBehaviour, IDamageable
    {
        [SerializeField] private HealthComponent lifeComponent;
        
        public void TakeDamage(int damage)
        {
            lifeComponent.TakeDamage(damage);    
        }
    }
}