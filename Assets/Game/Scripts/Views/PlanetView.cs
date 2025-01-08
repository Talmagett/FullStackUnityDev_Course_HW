using System;
using Modules.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        [SerializeField] private GameObject coinGameObject;
        [SerializeField] private GameObject incomeGameObject;
        
        [SerializeField] private GameObject priceGameObject;
        [SerializeField] private TMP_Text priceText;
        
        [SerializeField] private GameObject lockGameObject;

        [SerializeField] private TMP_Text incomeTimerText;
        [SerializeField] private Image incomeFillImage;
        
        [SerializeField] private SmartButton smartButton;
        
        public event Action OnClick
        {
            add => smartButton.OnClick += value;
            remove => smartButton.OnClick -= value;
        }
        
        public event Action OnHold
        {
            add => smartButton.OnHold += value;
            remove => smartButton.OnHold -= value;
        }
        
        [Button]
        public void SetTimerText(string remainingTimer)
        {
            incomeTimerText.text = remainingTimer;
        }
        
        [Button]
        public void SetIncomeSliderValue(float value)
        {
            incomeFillImage.fillAmount = value;
        }
        
        [Button]
        public void ShowCoinHideIncomeSlider(bool enable)
        {
            coinGameObject.SetActive(enable);
            incomeGameObject.SetActive(!enable);
        }

        [Button]
        public void SetPrice(string price)
        {
            priceText.text = price;
        }
        
        [Button]
        public void ShowPriceAndLock(bool enable)
        {
            priceGameObject.SetActive(enable);
            lockGameObject.SetActive(enable);
        }
    }
}