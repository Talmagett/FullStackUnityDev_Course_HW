using System;
using Game.Views;
using Modules.Money;
using Zenject;

namespace Game.Presenters
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        private IMoneyStorage _moneyStorage;
        private MoneyView _moneyView;

        public MoneyPresenter(IMoneyStorage moneyStorage, MoneyView moneyView)
        {
            _moneyStorage = moneyStorage;
            _moneyView = moneyView;
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
            _moneyView.SetMoneyText(newvalue.ToString());
        }
    }
}