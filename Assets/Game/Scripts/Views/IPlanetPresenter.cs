using System;
using UnityEngine;

namespace Game.Views
{
    public interface IPlanetPresenter
    {
        event Action OnStateChanged;
        Sprite PlanetIcon { get; }
        string PriceText { get; }
        string RemainingTimerText { get; }
        float IncomeProgressValue { get; }
        bool IsIncomeReady { get; }
        bool IsUnlocked { get; }
        void Click();
        void Hold();
    }
}