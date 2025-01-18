using System;
using Modules.Planets;
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
        event Action<int> OnUpgraded;
        event Action<int> OnIncomeChanged;
        event Action<int> OnPopulationChanged;
        event Action<int> OnMoneyChanged;
        void SetPlanet(IPlanet planet);
    }
}