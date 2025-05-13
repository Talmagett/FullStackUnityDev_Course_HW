using System;
using Atomic.Entities;
using Modules.Common;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;
using Atomic.Presenters;

using Game.Scripts.Gameplay.Context;
namespace Game.UI
{
    public class HealthPresenter : Presenter
    {
        [SerializeField] private StatView healthStatView;
        private Health _health;
        
        protected override void OnCreate()
        {
            GameContext gameContext = GameContext.Instance;
            _health = gameContext.GetPlayerCharacter().GetHealth();
            OnStateChanged();
        }

        protected override void OnInit()
        {
            _health.OnStateChanged+=OnStateChanged;
        }

        protected override void OnDispose()
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