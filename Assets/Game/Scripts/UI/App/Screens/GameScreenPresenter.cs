using Atomic.UI;
using Game.App.Audio.Music;
using Game.UI.App.Background;
using UnityEngine;
using Zenject;

namespace Game.UI.App.Screens
{
    public class GameScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private MusicName musicName;

        [Inject] private BackgroundView backgroundView;
        [Inject] private MusicPlayer musicPlayer;

        protected override void OnShow()
        {
            backgroundView.SetSprite(backgroundImage);
            musicPlayer.Play(musicName);
        }
    }
}