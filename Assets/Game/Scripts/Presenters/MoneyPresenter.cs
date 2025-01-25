using System;
using Game.Views;
using Modules.Money;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IMoneyPresenter, IInitializable, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly ParticleAnimator _particleAnimator;

        public string MoneyText => _moneyStorage.Money.ToString();
        public event Action OnMoneyChanged;
        public event Action<int, int> OnMoneyEarned;
        
        private Vector3 _moneyViewPosition;

        public MoneyPresenter(IMoneyStorage moneyStorage, ParticleAnimator particleAnimator)
        {
            _moneyStorage = moneyStorage;
            _particleAnimator = particleAnimator;
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneySpent += OnMoneySpent;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneySpent -= OnMoneySpent;
        }

        public void SetMoneyViewPosition(Vector3 position)
        {
            _moneyViewPosition = position;
        }
        
        private void OnMoneySpent(int newvalue, int range)
        {
            OnMoneyChanged?.Invoke();
        }

        public void GatherMoney(Vector3 planetPosition, int moneyValue)
        {
            _particleAnimator.Emit(planetPosition, _moneyViewPosition, 1,
                () => OnMoneyEarned?.Invoke(_moneyStorage.Money-moneyValue, _moneyStorage.Money));
        }

    }
}