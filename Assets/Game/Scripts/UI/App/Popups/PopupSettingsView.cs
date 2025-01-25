using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class PopupSettingsView : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button homeButton;

        public event UnityAction OnCloseButtonClicked
        {
            add => closeButton.onClick.AddListener(value);
            remove => closeButton.onClick.RemoveListener(value);
        }
        
        public event UnityAction OnHomeButtonClicked
        {
            add => homeButton.onClick.AddListener(value);
            remove => homeButton.onClick.RemoveListener(value);
        }
    }
}