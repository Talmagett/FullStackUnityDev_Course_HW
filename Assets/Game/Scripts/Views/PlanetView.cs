using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        [SerializeField] private Image planetIcon;
        
        [SerializeField] private GameObject coinGameObject;
        [SerializeField] private GameObject incomeGameObject;
        
        [SerializeField] private GameObject priceGameObject;
        [SerializeField] private TMP_Text priceText;
        
        [SerializeField] private GameObject lockGameObject;

        [SerializeField] private TMP_Text incomeTimerText;
        [SerializeField] private Image incomeFillImage;
        
        [SerializeField] private SmartButton smartButton;

        private IPlanetPresenter _planetPresenter;

        public void Construct(IPlanetPresenter planetPresenter)
        {
            _planetPresenter = planetPresenter;
            UpdateState();
        }
        
        private void OnEnable()
        {
            _planetPresenter.OnStateChanged += UpdateState;
            smartButton.OnClick += _planetPresenter.Click;
            smartButton.OnHold += _planetPresenter.Hold;
        }

        private void OnDisable()
        {
            _planetPresenter.OnStateChanged -= UpdateState;
            smartButton.OnClick -= _planetPresenter.Click;
            smartButton.OnHold -= _planetPresenter.Hold;
        }

        private void UpdateState()
        {
            planetIcon.sprite = _planetPresenter.PlanetIcon;
            priceText.text = _planetPresenter.PriceText;
            incomeTimerText.text = _planetPresenter.RemainingTimerText;
            incomeFillImage.fillAmount = _planetPresenter.IncomeProgressValue;
            coinGameObject.SetActive(_planetPresenter.IsIncomeReady&&_planetPresenter.IsUnlocked);
            incomeGameObject.SetActive(!_planetPresenter.IsIncomeReady&&_planetPresenter.IsUnlocked);
            priceGameObject.SetActive(!_planetPresenter.IsUnlocked);
            lockGameObject.SetActive(!_planetPresenter.IsUnlocked);
        }
        
        public Vector3 Position => coinGameObject.transform.position;
    }
}