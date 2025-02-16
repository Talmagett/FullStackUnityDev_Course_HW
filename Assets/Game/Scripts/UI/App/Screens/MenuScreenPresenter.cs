using Atomic.UI;
using Game.App.Audio.Music;
using Game.UI.App.Background;
using Game.UI.App.Screens.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.App.Screens
{
    public class MenuScreenPresenter : Presenter
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private MusicName musicName;

        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private BackgroundView backgroundView;
        [Inject] private MusicPlayer musicPlayer;

        protected override void OnInit()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        protected override void OnDispose()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        protected override void OnShow()
        {
            backgroundView.SetSprite(backgroundImage);
            musicPlayer.Play(musicName);
        }

        private void OnPlayButtonClicked()
        {
            screenNavigator.ChangeScreen(ScreenName.Levels);
        }
    }
}