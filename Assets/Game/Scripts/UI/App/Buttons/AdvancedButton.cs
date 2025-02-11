using Game.App;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Game.Scripts.UI.App.Buttons
{
    public class AdvancedButton : Button
    {
        [Inject] private SoundPlayer _soundPlayer;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            onClick.AddListener(PlayClickSound);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            onClick.RemoveListener(PlayClickSound);
        }

        private void PlayClickSound()
        {
            _soundPlayer.Play(SoundName.Click);
        }
    }
}