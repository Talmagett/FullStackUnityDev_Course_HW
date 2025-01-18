using System;
using Game.Views;
using Modules.Money;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IPlanetPopupPresenter
    {
        private readonly PlanetPopupView _view;
        private readonly IMoneyStorage _moneyStorage;
        
        private IPlanet _planet;
        
        public PlanetPopupPresenter(PlanetPopupView view, IMoneyStorage moneyStorage)
        {
            _view = view;
            _moneyStorage = moneyStorage;
        }

        public void Show(IPlanet planet)
        {
            _planet = planet;
            UpdateData();
            _view.Show();
            _view.OnCloseBtnClicked += Hide;
        }

        private void Hide()
        {
            _view.Hide();
            _planet.OnPopulationChanged -= OnPopulationChanged;
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            _view.OnUpgradeBtnClicked -= OnUpgradeBtnClicked;
            _planet.OnUpgraded -= OnUpgraded;
            _planet.OnIncomeChanged -= OnIncomeChanged;
            _view.OnCloseBtnClicked -= Hide;
        }
        
        private void UpdateData()
        {
            _view.SetPlanetAvatar(_planet.GetIcon(true));
            _view.SetPlanetNameText(_planet.Name);
            _view.SetPopulationText($"Population: {_planet.Population}");
            OnIncomeChanged(_planet.MinuteIncome);
            OnUpgraded(_planet.Level);
            OnMoneyChanged(_moneyStorage.Money,0);
            if(!_planet.IsUnlocked)
                _view.SetUpgradeButtonInteractable(false);
            _planet.OnPopulationChanged += OnPopulationChanged;
            _planet.OnUpgraded += OnUpgraded;
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
            _view.OnUpgradeBtnClicked += OnUpgradeBtnClicked;
            _planet.OnIncomeChanged += OnIncomeChanged;
        }

        private void OnIncomeChanged(int obj)
        {
            _view.SetIncomeText($"Income: {_planet.MinuteIncome}$");
        }

        private void OnUpgraded(int level)
        {
            _view.SetUpgradePriceText(_planet.Price.ToString());
            _view.SetLevelText($"Level: {level}/{_planet.MaxLevel}");
            _view.SetUpgradeText(_planet.IsMaxLevel?"MAX LEVEL": "UPGRADE");
            _view.SetPriceGameObjectActive(!_planet.IsMaxLevel); 
            
            _view.SetUpgradeButtonInteractable(!_planet.IsMaxLevel&&_planet.Price<=_moneyStorage.Money);
        }

        private void OnUpgradeBtnClicked()
        {
            if (!_planet.CanUpgrade) return;
            
            _planet.Upgrade();
        }

        private void OnPopulationChanged(int population)
        {
            _view.SetPopulationText($"Population: {population}");
        }

        private void OnMoneyChanged(int newValue, int prevValue)
        {
            if (_planet.IsMaxLevel) 
            {
                _view.SetUpgradeButtonInteractable(false);
                return;
            }
            
            var canUpgrade = _planet.Price<=newValue;
            _view.SetUpgradeButtonInteractable(canUpgrade);
        }
    }
}