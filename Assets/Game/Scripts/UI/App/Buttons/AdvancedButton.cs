using Game.App.Audio.Sound;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.App.Buttons
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