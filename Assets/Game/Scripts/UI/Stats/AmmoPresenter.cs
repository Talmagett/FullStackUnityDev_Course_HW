using UnityEngine;
using TMPro;
using Game.Scripts.Gameplay.Context;
using Atomic.Elements;
using SampleGame;
using Modules.Gameplay;

namespace Game.UI
{
    public class AmmoPresenter : MonoBehaviour
    {
        [SerializeField] private StatView ammoStat;
        private Ammo _ammo;
        private void Awake()
        {
            GameContext gameContext = GameContext.Instance;
            _ammo = gameContext.GetPlayerCharacter().GetCurrentWeapon().Value.GetAmmo();
            OnStateChanged();
        }
        void OnEnable()
        {
            _ammo.OnStateChanged += OnStateChanged;
        }
        void OnDisable()
        {            
            _ammo.OnStateChanged -= OnStateChanged;

        }
        private void OnStateChanged()
        {
            ammoStat.SetText(_ammo.GetCount().ToString());
        }        
    }
}