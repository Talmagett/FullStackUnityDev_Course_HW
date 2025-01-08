using System;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IInitializable,IDisposable
    {
        private IPlanet _planet;
        private readonly PlanetPopupView _view;

        public PlanetPopupPresenter(PlanetPopupView view)
        {
            _view = view;
        }

        public void SetPlanet(IPlanet planet)
        {
            _planet = planet;
            UpdateData();
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
        }

        public void Initialize()
        {
            _planet.OnPopulationChanged += UpdatePopulationText;
        }

        public void Dispose()
        {
            _planet.OnPopulationChanged -= UpdatePopulationText;
        }
        
        private void UpdatePopulationText(int population)
        {
            _view.SetPopulationText($"Population: {population}");
        }
    }
}