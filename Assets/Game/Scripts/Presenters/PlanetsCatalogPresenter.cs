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
        private readonly List<PlanetPresenter> _presenters=new ();
        private readonly PlanetPresenter.Factory _factory;
        
        public PlanetsCatalogPresenter(IPlanet[] planets, PlanetView[] planetViews, PlanetPresenter.Factory factory)
        {
            _planets = planets;
            _planetViews = planetViews;
            _factory = factory;

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
                var planetPresenter = _factory.Create(_planets[i],_planetViews[i].Position);
                _presenters.Add(planetPresenter);
                _planetViews[i].Construct(planetPresenter);
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