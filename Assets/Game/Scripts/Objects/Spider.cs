using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    public class Spider : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;

        private void OnEnable()
        {
            healthComponent.OnDead += OnHealthEmpty;
        }

        private void OnDisable()
        {
            healthComponent.OnDead -= OnHealthEmpty;
        }

        
        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }
    }
}