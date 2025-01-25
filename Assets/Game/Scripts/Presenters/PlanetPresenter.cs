using System;
using Game.Views;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IPlanetPresenter, IInitializable, IDisposable
    {
        private readonly IPlanet _planet;
        private readonly PlanetPopupShower _popupShower;
        private readonly MoneyPresenter _moneyPresenter;
        public event Action OnStateChanged;
        private int _remainingTime;
        public Sprite PlanetIcon => _planet.GetIcon(_planet.IsUnlocked);
        public string PriceText => _planet.Price.ToString();
        public string RemainingTimerText => $"{_remainingTime / 60}m:{_remainingTime % 60}s";
        public float IncomeProgressValue => _planet.IncomeProgress;
        public bool IsIncomeReady => _planet.IsIncomeReady;
        public bool IsUnlocked => _planet.IsUnlocked;
        private Vector3 _viewMoneyPosition;
        
        public PlanetPresenter(IPlanet planet, PlanetPopupShower popupShower, MoneyPresenter moneyPresenter, Vector3 viewMoneyPosition)
        {
            _planet = planet;
            _popupShower = popupShower;
            _moneyPresenter = moneyPresenter;
            _viewMoneyPosition = viewMoneyPosition;
        }

        public void Initialize()
        {
            _planet.OnIncomeTimeChanged += OnIncomeTimeChanged;
            _planet.OnIncomeReady += OnIncomeReady;
            _planet.OnUnlocked += OnUnlocked;
            _planet.OnGathered+=OnGathered;
        }
        
        public void Dispose()
        {
            _planet.OnIncomeTimeChanged -= OnIncomeTimeChanged;
            _planet.OnIncomeReady -= OnIncomeReady;
            _planet.OnUnlocked -= OnUnlocked;
            _planet.OnGathered-=OnGathered;
        }

        private void OnIncomeReady(bool obj)
        {
            OnStateChanged?.Invoke();
        }

        public void Click()
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

        public void Hold()
        {
            _popupShower.Show(_planet);
        }
        
        private void OnGathered(int moneyValue)
        {
            _moneyPresenter.GatherMoney(_viewMoneyPosition, moneyValue);
        }

        private void OnUnlocked()
        {
            OnStateChanged?.Invoke();
        }

        private void OnIncomeTimeChanged(float time)
        {
            _remainingTime = (int)time;
            OnStateChanged?.Invoke();
        }

        public class Factory : PlaceholderFactory<IPlanet,Vector3, PlanetPresenter>
        {
            
        }
    }
}