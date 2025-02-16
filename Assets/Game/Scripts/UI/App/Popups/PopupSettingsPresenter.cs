using Atomic.UI;
using Game.App.Audio.Music;
using Game.App.Audio.Sound;
using Game.App.Scene;
using Game.UI.App.Screens.Manager;
using UnityEngine;
using Zenject;

namespace Game.UI.App.Popups
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

        protected override void OnShow()
        {
            popupSettingsView.SetSoundSliderValue(_soundPlayer.GetVolume());
            popupSettingsView.SetMusicSliderValue(_musicPlayer.GetVolume());
        }

        private void GoHome()
        {
            Hide();
            _screenNavigator.ChangeScreen(ScreenName.Menu);
            _sceneNavigator.OpenMenu();
        }
    }
}