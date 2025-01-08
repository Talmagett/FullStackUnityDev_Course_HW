using System;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly IPlanet _planet;
        private readonly PlanetView _view;

        public PlanetPresenter(IPlanet planet, PlanetView view)
        {
            _planet = planet;
            _view = view;

            InitData();
        }

        private void InitData()
        {
            var isUnlocked = _planet.IsUnlocked;
            
            _view.ShowPriceAndLock(!isUnlocked);
            if (isUnlocked)
                _view.SetPrice(_planet.Price.ToString());

            _view.SetIcon(_planet.GetIcon(isUnlocked));
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            
        }
    }
}