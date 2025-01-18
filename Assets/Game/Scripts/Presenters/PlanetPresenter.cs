using System;
using Game.Views;
using Modules.Planets;
using Modules.UI;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly IPlanet _planet;
        private readonly PlanetView _view;
        private readonly IPlanetPopupPresenter _popupPresenter;
        private readonly MoneyPresenter _moneyPresenter;
        
        public PlanetPresenter(IPlanet planet, PlanetView view, IPlanetPopupPresenter popupPresenter, MoneyPresenter moneyPresenter)
        {
            _planet = planet;
            _view = view;
            _popupPresenter = popupPresenter;
            _moneyPresenter = moneyPresenter;
            InitData();
        }

        private void InitData()
        {
            _view.SetPrice(_planet.Price.ToString());
            _view.SetIcon(_planet.GetIcon(false));
            _view.SetActiveIncomeSlider(false);
            _view.SetActiveCoin(false);
        }

        public void Initialize()
        {
            _planet.OnIncomeTimeChanged += OnIncomeTimeChanged;
            _planet.OnIncomeReady += OnIncomeReady;
            _planet.OnUnlocked += OnUnlocked;
            _planet.OnGathered+=OnGathered;
            _view.OnClick += OnClick;
            _view.OnHold += OnHold;
        }
        
        public void Dispose()
        {
            _planet.OnIncomeTimeChanged -= OnIncomeTimeChanged;
            _planet.OnIncomeReady -= OnIncomeReady;
            _planet.OnUnlocked -= OnUnlocked;
            _planet.OnGathered-=OnGathered;
            _view.OnClick -= OnClick;
            _view.OnHold -= OnHold;
        }

        private void OnGathered(int moneyValue)
        {
            _moneyPresenter.GatherMoney(_view.Position,moneyValue);
        }

        private void OnUnlocked()
        {
            _view.HidePriceAndLock();
            _view.SetIcon(_planet.GetIcon(true));
            _view.SetActiveIncomeSlider(true);
        }

        private void OnClick()
        {
            if(_planet.IsUnlocked)
            {
                _planet.GatherIncome();
            }
            else
            {
                if(_planet.CanUnlock)
                    _planet.Unlock();
            }
        }
        
        private void OnHold()
        {
            _popupPresenter.Show(_planet);
        }
        
        private void OnIncomeReady(bool incomeReady)
        {
            _view.SetActiveCoin(incomeReady);
            _view.SetActiveIncomeSlider(!incomeReady);
        }

        private void OnIncomeTimeChanged(float time)
        {
            var timeInt = (int)time;
            _view.SetTimerText($"{timeInt/60}m:{timeInt%60}s");
            _view.SetIncomeSliderValue(_planet.IncomeProgress);
        }

        public class Factory : PlaceholderFactory<IPlanet, PlanetView, PlanetPresenter>
        {
            
        }
    }
}