using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

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

        [SerializeField] private TMP_Text upgradeText;
        [SerializeField] private GameObject priceGameObject;
        [SerializeField] private TMP_Text upgradePriceText;
        [SerializeField] private Button upgradeButton;
        
        [Inject] private IPlanetPopupPresenter popupPresenter;
        private event UnityAction OnCloseBtnClicked
        {
            add => closeButton.onClick.AddListener(value);
            remove => closeButton.onClick.RemoveListener(value);
        }
        private event UnityAction OnUpgradeBtnClicked
        {
            add => upgradeButton.onClick.AddListener(value);
            remove => upgradeButton.onClick.RemoveListener(value);
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            SetInitData();
        }

        private void OnEnable()
        {
            OnCloseBtnClicked += Hide;
            OnUpgradeBtnClicked += popupPresenter.Upgrade;
            popupPresenter.OnUpgraded += OnUpgraded;
            popupPresenter.OnMoneyChanged+=OnMoneyChanged;
            popupPresenter.OnPopulationChanged += OnPopulationChanged;
            popupPresenter.OnIncomeChanged += OnIncomeChanged;
        }

        private void OnDisable()
        {
            OnCloseBtnClicked -= Hide;
            OnUpgradeBtnClicked -= popupPresenter.Upgrade;
            popupPresenter.OnUpgraded -= OnUpgraded;
            popupPresenter.OnMoneyChanged-=OnMoneyChanged;
            popupPresenter.OnPopulationChanged -= OnPopulationChanged;
            popupPresenter.OnIncomeChanged -= OnIncomeChanged;
        }

        private void OnMoneyChanged(int _=0)
        {
            OnUpgraded();
        }

        private void OnPopulationChanged(int _=0)
        {
            SetPopulationText(popupPresenter.PopulationText);
        }

        private void OnIncomeChanged(int _=0)
        {
            SetIncomeText(popupPresenter.IncomeText);
        }

        private void SetInitData()
        {
            SetPlanetNameText(popupPresenter.PlanetName);
            SetPlanetAvatar(popupPresenter.PlanetAvatar);

            OnPopulationChanged();
            OnIncomeChanged();
            OnUpgraded();
        }

        private void OnUpgraded(int _=0)
        {
            SetLevelText(popupPresenter.LevelText);
            SetUpgradeText(popupPresenter.UpgradeText);
            
            SetPriceGameObjectActive(popupPresenter.PriceGameObjectActive);
            SetUpgradePriceText(popupPresenter.UpgradePriceText);
            SetUpgradeButtonInteractable(popupPresenter.UpgradeButtonActive);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        [Button]
        private void SetPlanetNameText(string planetName)
        {
            planetNameText.text = planetName;
        }

        [Button]
        private void SetPlanetAvatar(Sprite avatar)
        {
            planetAvatar.sprite = avatar;
        }

        [Button]
        private void SetPopulationText(string population)
        {
            populationText.text = population;
        }

        [Button]
        private void SetLevelText(string level)
        {
            levelText.text = level;
        }

        [Button]
        private void SetIncomeText(string income)
        {
            incomeText.text = income;
        }

        [Button]
        private void SetUpgradeText(string text)
        {
            upgradeText.text = text;
        }

        [Button]
        private void SetPriceGameObjectActive(bool active)
        {
            priceGameObject.SetActive(active);
        }
        
        [Button]
        private void SetUpgradePriceText(string price)
        {
            upgradePriceText.text = price;
        }

        [Button]
        private void SetUpgradeButtonInteractable(bool interactable)
        {
            upgradeButton.interactable = interactable;
        }
    }
}