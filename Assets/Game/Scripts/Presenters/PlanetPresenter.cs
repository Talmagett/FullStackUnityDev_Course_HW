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
        private readonly PlanetPopupPresenter _popupPresenter;

        public PlanetPresenter(IPlanet planet, PlanetView view, PlanetPopupPresenter popupPresenter)
        {
            _planet = planet;
            _view = view;
            _popupPresenter = popupPresenter;

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
            _planet.OnIncomeTimeChanged += UpdateTimerText;
            _planet.OnIncomeReady += UpdateIncomeReady;
            _view.OnClick += OnClick;
            _view.OnHold += OnHold;
        }
        
        public void Dispose()
        {
            _planet.OnIncomeTimeChanged -= UpdateTimerText;
            _planet.OnIncomeReady -= UpdateIncomeReady;
        }
        
        private void OnClick()
        {
            if(_planet.CanUnlock)
                _planet.Unlock();
        }
        
        private void OnHold()
        {
            _popupPresenter.Show(_planet);
        }
        
        private void UpdateIncomeReady(bool incomeReady)
        {
            _view.ShowCoinHideIncomeSlider(incomeReady);
        }

        private void UpdateTimerText(float time)
        {
            _view.SetTimerText($"{time/60}m:{time%60}s");
            _view.SetIncomeSliderValue(_planet.IncomeProgress);
        }
    }
}