using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _damage = 2;
        
        private void OnTriggerEnter(Collider other)
        {
            //Работа через прокси
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }
        }
    }
}