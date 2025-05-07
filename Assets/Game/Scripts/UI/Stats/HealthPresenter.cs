using System;
using Atomic.Entities;
using Modules.Common;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

using Game.Scripts.Gameplay.Context;
namespace Game.UI
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private StatView healthStatView;
        private Health _health;
        
        private void Awake()
        {
            GameContext gameContext = GameContext.Instance;
            _health = gameContext.GetPlayerCharacter().GetHealth();
            OnStateChanged();
        }

        private void OnEnable()
        {
            _health.OnStateChanged+=OnStateChanged;
        }

        private void OnDisable()
        {
            _health.OnStateChanged-=OnStateChanged;
        }

        private void OnStateChanged()
        {
            var healthPercent = _health.GetPercent();
            healthStatView.SetProgress(healthPercent);
            healthStatView.SetText(_health.GetCurrent().ToString());
            healthStatView.SetVisible(!_health.IsEmpty());
        }
    }
}