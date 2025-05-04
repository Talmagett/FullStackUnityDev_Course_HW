using Atomic.Entities;
using Modules.Common;
using SampleGame;
using UnityEngine;

namespace Game.UI
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private HealthScreen healthScreen;
        [SerializeField] private SceneEntity playerEntity;

        private void OnEnable()
        {
            playerEntity.GetHealth().Subscribe(OnHealthChanged);
        }

        private void OnDisable()
        {
            playerEntity.GetHealth().Unsubscribe(OnHealthChanged);
        }
        
        private void OnHealthChanged(int health)
        {
            var healthPercent = (float)health / playerEntity.GetMaxHealth().Value;
            healthScreen.TakeDamage(health);
            healthScreen.ChangePercent(healthPercent);
        }
    }
}