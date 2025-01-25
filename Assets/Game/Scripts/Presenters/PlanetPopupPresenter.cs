using System;
using Game.Views;
using Modules.Money;
using Modules.Planets;
using UnityEngine;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IPlanetPopupPresenter, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private IPlanet _planet;
        public event Action OnStateChanged;

        public PlanetPopupPresenter(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _moneyStorage.OnMoneyChanged += OnMoneyChangedHandler;
        }
        
        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChangedHandler;
            if (_planet == null)
                return;
            _planet.OnUpgraded -= OnUpgraded;
            _planet.OnIncomeChanged -= OnIncomeChanged;
            _planet.OnPopulationChanged -= OnPopulationChanged;
        }
        
        public string PlanetName => _planet.Name;
        public Sprite PlanetAvatar => _planet.GetIcon(_planet.IsUnlocked);
        public string PopulationText => $"Population: {_planet.Population}";
        public string LevelText => $"Level: {_planet.Level}/{_planet.MaxLevel}";
        public string IncomeText => $"Income: {_planet.MinuteIncome}$";
        public string UpgradeText => _planet.IsMaxLevel ? "MAX LEVEL" : "UPGRADE";
        public bool PriceGameObjectActive => !_planet.IsMaxLevel;
        public string UpgradePriceText => _planet.Price.ToString();
        public bool UpgradeButtonActive => !_planet.IsMaxLevel && _planet.Price <= _moneyStorage.Money&&_planet.IsUnlocked;
        
        private void OnMoneyChangedHandler(int newvalue, int prevvalue)
        {
            OnStateChanged?.Invoke();
        }

        private void OnUpgraded(int level)
        {
            OnStateChanged?.Invoke();
        }

        private void OnIncomeChanged(int income)
        {
            OnStateChanged?.Invoke();
        }

        private void OnPopulationChanged(int population)
        {
            OnStateChanged?.Invoke();
        }
        
        public void SetPlanet(IPlanet planet)
        {
            _planet = planet;
        }
        
        public void Upgrade()
        {
            if (!_planet.CanUpgrade) return;
            _planet.Upgrade();
            OnStateChanged?.Invoke();
        }
    }
}