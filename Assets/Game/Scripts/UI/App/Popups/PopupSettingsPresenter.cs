using Atomic.UI;
using Game.App;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Views
{
    public class PopupSettingsPresenter : Presenter
    {
        [SerializeField] private PopupSettingsView popupSettingsView;
        
        [Inject] private SceneNavigator _sceneNavigator;
        [Inject] private ScreenNavigator _screenNavigator;

        [Inject] private SoundPlayer _soundPlayer;
        [Inject] private MusicPlayer _musicPlayer;
        
        protected override void OnInit()
        {
            popupSettingsView.OnCloseButtonClicked += Hide;
            popupSettingsView.OnHomeButtonClicked += GoHome;
            popupSettingsView.OnSoundSliderChanged += _soundPlayer.SetVolume;
            popupSettingsView.OnMusicSliderChanged += _musicPlayer.SetVolume;
            Hide();
        }

        protected override void OnDispose()
        {
            popupSettingsView.OnCloseButtonClicked -= Hide;
            popupSettingsView.OnHomeButtonClicked -= GoHome;
            popupSettingsView.OnSoundSliderChanged -= _soundPlayer.SetVolume;
            popupSettingsView.OnMusicSliderChanged -= _musicPlayer.SetVolume;
        }

        private void GoHome()
        {
            Hide();
            _screenNavigator.ChangeScreen(ScreenName.Menu);
            _sceneNavigator.OpenMenu();
        }
    }
}