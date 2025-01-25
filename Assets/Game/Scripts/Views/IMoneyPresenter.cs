using System;
using UnityEngine;

namespace Game.Views
{
    public interface IMoneyPresenter
    {
        string MoneyText { get; }
        event Action OnMoneyChanged;
        event Action<int,int> OnMoneyEarned;
        void SetMoneyViewPosition(Vector3 position);
    }
}