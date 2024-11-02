using UnityEngine;

namespace Components
{
    [System.Serializable]
    public class HealthComponent
    {
        public event System.Action OnHealthEmpty;

        [SerializeField] private int health;
        private int _health;

        public void Init()
        {
            _health = health;
        }
        
        public void TakeDamage(int damage)
        {
            if (_health <= 0)
                return;

            if (damage <= 0)
                return;

            _health = Mathf.Max(0, _health - damage);
            if (_health == 0)
                OnHealthEmpty?.Invoke();
        }
    }
}