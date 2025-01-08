using System;
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
            UpdateMoneyText(_moneyStorage.Money,0);
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += UpdateMoneyText;
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= UpdateMoneyText;
        }
        
        private void UpdateMoneyText(int newvalue, int prevvalue)
        {
            _view.SetMoneyText(newvalue.ToString());
        }
    }
}