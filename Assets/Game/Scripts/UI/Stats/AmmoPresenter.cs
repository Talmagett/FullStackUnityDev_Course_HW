using UnityEngine;
using TMPro;
using Game.Scripts.Gameplay.Context;
using Atomic.Elements;
using SampleGame;
using Modules.Gameplay;
using Atomic.Presenters;

namespace Game.UI
{
    public class AmmoPresenter : Presenter
    {
        [SerializeField] private StatView ammoStat;
        private Ammo _ammo;
        protected override void OnCreate()
        {
            GameContext gameContext = GameContext.Instance;
            _ammo = gameContext.GetPlayerCharacter().GetCurrentWeapon().Value.GetAmmo();
            OnStateChanged();
        }
        protected override void OnInit()
        {
            _ammo.OnStateChanged += OnStateChanged;
        }
        protected override void OnDispose()
        {            
            _ammo.OnStateChanged -= OnStateChanged;

        }
        private void OnStateChanged()
        {
            ammoStat.SetText(_ammo.GetCount().ToString());
        }        
    }
}