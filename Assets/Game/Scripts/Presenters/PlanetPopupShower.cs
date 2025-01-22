using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetPopupShower
    {
        private readonly PlanetPopupView _view;
        private readonly PlanetPopupPresenter _presenter;

        public PlanetPopupShower(PlanetPopupView view, PlanetPopupPresenter presenter)
        {
            _view = view;
            _presenter = presenter;
        }

        public void Show(IPlanet planet)
        {
            //_presenter.SetPlanet(planet);
            _view.Show();
        }
    }
}