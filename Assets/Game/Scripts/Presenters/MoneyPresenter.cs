using System;
using Game.Views;
using Modules.Money;
using Modules.UI;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly MoneyView _view;
        private readonly ParticleAnimator _particleAnimator;

        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView view, ParticleAnimator particleAnimator)
        {
            _moneyStorage = moneyStorage;
            _view = view;
            _particleAnimator = particleAnimator;
            _view.SetMoneyText(_moneyStorage.Money.ToString());
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneySpent += OnMoneySpent;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneySpent -= OnMoneySpent;
        }

        private void OnMoneyEarned(int newvalue, int range)
        {
            _view.SetMoneyTextWithAnimation(newvalue - range, newvalue);
        }

        private void OnMoneySpent(int newvalue, int range)
        {
            _view.SetMoneyText(newvalue.ToString());
        }

        public void GatherMoney(Vector3 planetPosition, int moneyValue)
        {
            _particleAnimator.Emit(planetPosition, _view.Position, 1,
                () => OnMoneyEarned(_moneyStorage.Money, moneyValue));
        }
    }
}