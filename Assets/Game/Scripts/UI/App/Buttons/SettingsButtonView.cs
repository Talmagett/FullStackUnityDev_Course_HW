using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.UI.App.Buttons
{
    public class SettingsButtonView : MonoBehaviour
    {
        [SerializeField] private Button settingsButton;
        public event UnityAction OnSettingsButtonClicked
        {
            add => settingsButton.onClick.AddListener(value);
            remove => settingsButton.onClick.RemoveListener(value);
        }
    }
}