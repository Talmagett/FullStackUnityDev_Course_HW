using System;
using DG.Tweening;
using Game.Views;
using Modules.Money;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private readonly IMoneyStorage _moneyStorage;
        private readonly MoneyView _view;
        
        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView view)
        {
            _moneyStorage = moneyStorage;
            _view = view;
            _view.SetMoneyText(_moneyStorage.Money.ToString());
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
            _moneyStorage.OnMoneyEarned+=OnMoneyEarned;
            _moneyStorage.OnMoneySpent+=OnMoneySpent;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
            _moneyStorage.OnMoneyEarned+=OnMoneyEarned;
            _moneyStorage.OnMoneySpent+=OnMoneySpent;
        }

        private void OnMoneyChanged(int newvalue, int prevvalue)
        {
            _view.SetMoneyText(newvalue.ToString());
        }

        private void OnMoneyEarned(int newvalue, int range)
        {
            DOTween.To(() => newvalue-range, x => _view.SetMoneyText(x.ToString()), newvalue, 0.5f);
        }

        private void OnMoneySpent(int newvalue, int range)
        {
            _view.SetMoneyText(newvalue.ToString());
        }
    }
}