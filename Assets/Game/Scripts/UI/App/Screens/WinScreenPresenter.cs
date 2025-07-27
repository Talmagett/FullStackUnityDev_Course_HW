using Atomic.UI;
using Game.App.Audio.Music;
using Game.App.Audio.Sound;
using Game.App.Levels;
using Game.Gameplay.Items;
using Game.Gameplay.Quests;
using Game.UI.App.Background;
using Game.UI.App.Level;
using Game.UI.App.Screens.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.App.Screens
{
    public class WinScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        
        [SerializeField] private LevelNumberView levelNumberView;
        [SerializeField] private Image questTarget;
        [SerializeField] private Button nextButton;
        
        [Inject] private ILevelService _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        [Inject] private Quest _quest;//change?
        [Inject] private BackgroundView backgroundView;
        [Inject] private MusicPlayer musicPlayer;
        [Inject] private SoundPlayer soundPlayer;
        [Inject] private ScreenNavigator _screenNavigator;
        
        protected override void OnInit()
        {
            nextButton.onClick.AddListener(OnNextButtonClicked);
        }

        protected override void OnDispose()
        {
            nextButton.onClick.RemoveListener(OnNextButtonClicked);
        }

        private void OnNextButtonClicked()
        {
            _screenNavigator.ChangeScreen(ScreenName.Levels);
        }

        protected override void OnShow()
        {
            levelNumberView.SetNumberImage(_map.CurrentLevel.NumberIcon);
            questTarget.sprite = _itemSpriteMap.GetQuestSprite(_map.CurrentLevel.GoalType);
            musicPlayer.SetMute(true);
            soundPlayer.Play(SoundName.Win);
            backgroundView.SetSprite(backgroundImage);
            //change
            _map.OnFinishLevel();
        }
    }
}