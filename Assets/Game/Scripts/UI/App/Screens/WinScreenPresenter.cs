using Atomic.UI;
using Game.App;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.UI.App.Level;
using Game.System.Gameplay.Quests;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class WinScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        
        [SerializeField] private LevelNumberView levelNumberView;
        [SerializeField] private Image questTarget;
        [SerializeField] private Button nextButton;
        
        [Inject] private IMap _map;
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