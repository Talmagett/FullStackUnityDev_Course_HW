using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text planetNameText;
        [SerializeField] private Button closeButton;
        [SerializeField] private Image planetAvatar;
        
        [SerializeField] private TMP_Text populationText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text incomeText;
        
        [SerializeField] private TMP_Text upgradePriceText;
        [SerializeField] private Button upgradeButton;
        
        public event UnityAction OnCloseBtnClicked
        {
            add => closeButton.onClick.AddListener(value);
            remove => closeButton.onClick.RemoveListener(value);
        }
        public event UnityAction OnUpgradeBtnClicked
        {
            add => upgradeButton.onClick.AddListener(value);
            remove => upgradeButton.onClick.RemoveListener(value);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        [Button]
        public void SetPlanetNameText(string planetName)
        {
            this.planetNameText.text = planetName;
        }
        
        [Button]
        public void SetPlanetAvatar(Sprite avatar)
        {
            planetAvatar.sprite = avatar;
        }
        
        [Button]
        public void SetPopulationText(string population)
        {
            populationText.text = population;
        }
        
        [Button]
        public void SetLevelText(string level)
        {
            levelText.text = level;
        }
        
        [Button]
        public void SetIncomeText(string income)
        {
            incomeText.text = income;
        }
        
        [Button]
        public void SetUpgradePriceText(string price)
        {
            upgradePriceText.text = price;
        }
        
        [Button]
        public void SetUpgradeButtonInteractable(bool interactable)
        {
            upgradeButton.interactable = interactable;
        }

    }
}