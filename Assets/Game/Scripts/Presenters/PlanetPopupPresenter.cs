using System;
using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetPopupPresenter
    {
        private IPlanet _planet;
        private readonly PlanetPopupView _view;

        public PlanetPopupPresenter(PlanetPopupView view)
        {
            _view = view;
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
            _planet.OnPopulationChanged -= UpdatePopulationText;
            _view.OnCloseBtnClicked -= Hide;
        }
        
        private void UpdateData()
        {
            _view.SetPlanetAvatar(_planet.GetIcon(true));
            _view.SetPlanetNameText(_planet.Name);
            _view.SetPopulationText($"Population: {_planet.Population}");
            _view.SetLevelText($"Level: {_planet.Level}/{_planet.MaxLevel}");
            _view.SetIncomeText($"Income: {_planet.MinuteIncome/60}/sec");
            _view.SetUpgradePriceText(_planet.Price.ToString());
            _view.SetUpgradeButtonInteractable(!_planet.IsMaxLevel);
            _planet.OnPopulationChanged += UpdatePopulationText;
        }
        
        private void UpdatePopulationText(int population)
        {
            _view.SetPopulationText($"Population: {population}");
        }
    }
}