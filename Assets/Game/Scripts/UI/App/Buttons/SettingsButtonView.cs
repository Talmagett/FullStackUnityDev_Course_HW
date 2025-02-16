using Game.UI.App.Popups;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.App.Buttons
{
    public class SettingsButtonView : MonoBehaviour
    {
        [SerializeField] private Button settingsButton;
        [Inject] private PopupSettingsPresenter popupSettingsPresenter;

        private void OnEnable()
        {
            settingsButton.onClick.AddListener(popupSettingsPresenter.Show);
        }

        private void OnDisable()
        {
            settingsButton.onClick.RemoveListener(popupSettingsPresenter.Show);
        }

        public event UnityAction OnSettingsButtonClicked
        {
            add => settingsButton.onClick.AddListener(value);
            remove => settingsButton.onClick.RemoveListener(value);
        }
    }
}