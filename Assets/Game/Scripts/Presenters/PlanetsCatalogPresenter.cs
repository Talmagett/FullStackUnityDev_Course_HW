using System;
using System.Collections.Generic;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetsCatalogPresenter : IInitializable, IDisposable
    {
        private readonly IPlanet[] _planets;
        private readonly PlanetView[] _planetViews;
        private readonly PlanetPopupPresenter _planetPopupPresenter;
        private readonly List<PlanetPresenter> _presenters=new ();
        
        public PlanetsCatalogPresenter(IPlanet[] planets, PlanetView[] planetViews, PlanetPopupPresenter planetPopupPresenter)
        {
            _planets = planets;
            _planetViews = planetViews;
            _planetPopupPresenter = planetPopupPresenter;

            if(_planets.Length!=_planetViews.Length)
                throw new ArgumentException(
                    "Number of planets and planet views must match" +
                    $"{planets.Length} and {planetViews.Length}");

            CreatePresenters();
        }

        private void CreatePresenters()
        {
            for (int i = 0; i < _planets.Length; i++)
            {
                var planetPresenter = new PlanetPresenter(_planets[i],_planetViews[i],_planetPopupPresenter);
                _presenters.Add(planetPresenter);
            }
        }

        public void Initialize()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Initialize();
            }
        }

        public void Dispose()
        {
            foreach (var presenter in _presenters)
            {
                presenter.Dispose();
            }
            _presenters.Clear();
        }
    }
}