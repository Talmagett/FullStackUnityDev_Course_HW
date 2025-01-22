using System;
using UnityEngine;

namespace Game.Views
{
    public interface IPlanetPopupPresenter
    {
        string PlanetName { get; }
        Sprite PlanetAvatar { get; }
        string PopulationText { get; }
        string LevelText { get; }
        string IncomeText { get; }
        string UpgradeText { get; }
        bool PriceGameObjectActive { get; }
        string UpgradePriceText { get; }
        bool UpgradeButtonActive { get; }
        void Upgrade();
        event Action OnStateChanged;
    }
}