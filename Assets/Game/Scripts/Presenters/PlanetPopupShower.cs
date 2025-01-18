using Modules.Planets;
using UnityEngine;

namespace Game.Views
{
    public class PlanetPopupShower
    {
        private readonly PlanetPopupView _view;
        private readonly IPlanetPopupPresenter _presenter;

        public PlanetPopupShower(PlanetPopupView view, IPlanetPopupPresenter presenter)
        {
            _view = view;
            _presenter = presenter;
        }

        public void Show(IPlanet planet)
        {
            _presenter.SetPlanet(planet);
            _view.Show();
        }
    }
}