using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.UI.App.Sliders
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        public event UnityAction<float> OnSliderValueChanged
        {
            add => slider.onValueChanged.AddListener(value);
            remove => slider.onValueChanged.RemoveListener(value);
        }
    }
}