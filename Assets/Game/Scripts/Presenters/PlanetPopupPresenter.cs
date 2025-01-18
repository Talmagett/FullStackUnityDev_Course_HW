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
        
        public PlanetPopupPresenter(IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _moneyStorage.OnMoneyChanged += OnMoneyChangedHandler;
        }
        
        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged += OnMoneyChangedHandler;
        }
        //Может просто OnStateChanged использовать? вместо всех этих Actionов
        public event Action<int> OnUpgraded
        {
            add => _planet.OnUpgraded += value;
            remove=> _planet.OnUpgraded -= value;
        }
        public event Action<int> OnIncomeChanged
        {
            add => _planet.OnIncomeChanged += value;
            remove=> _planet.OnIncomeChanged -= value;
        }
        public event Action<int> OnPopulationChanged
        {
            add => _planet.OnPopulationChanged += value;
            remove => _planet.OnPopulationChanged -= value;
        }

        public event Action<int> OnMoneyChanged;

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
            OnMoneyChanged?.Invoke(newvalue);
        }
        
        public void SetPlanet(IPlanet planet)
        {
            _planet = planet;
        }
        
        public void Upgrade()
        {
            if (!_planet.CanUpgrade) return;
            _planet.Upgrade();
        }
    }
}