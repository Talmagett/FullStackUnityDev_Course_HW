using System;
using Game.Scripts.UI.App.Sliders;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.UI.Views
{
    public class PopupSettingsView : MonoBehaviour
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button homeButton;
        [SerializeField] private SliderView soundSlider;
        [SerializeField] private SliderView musicSlider;
        
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

        public event Action<float> OnSoundSliderChanged;
        public event Action<float> OnMusicSliderChanged;
        
        private void OnEnable()
        {
            soundSlider.OnSliderValueChanged += OnSoundSliderValueChanged;
            musicSlider.OnSliderValueChanged += OnMusicSliderValueChanged;
        }

        private void OnDisable()
        {
            soundSlider.OnSliderValueChanged -= OnSoundSliderValueChanged;
            musicSlider.OnSliderValueChanged -= OnMusicSliderValueChanged;
        }

        private void OnMusicSliderValueChanged(float volume)
        {
            OnMusicSliderChanged?.Invoke(volume);
        }

        private void OnSoundSliderValueChanged(float volume)
        {
            OnSoundSliderChanged?.Invoke(volume);
        }
        public void SetSoundSliderValue(float value)
        {
            soundSlider.SetValue(value);
        }
        public void SetMusicSliderValue(float value)
        {
            musicSlider.SetValue(value);
        }
    }
}