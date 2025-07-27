using Atomic.UI;
using Cysharp.Threading.Tasks;
using Game.App.Audio.Music;
using Game.App.Levels;
using Game.App.Scene;
using Game.Gameplay.Items;
using Game.UI.App.Background;
using Game.UI.App.Screens.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.App.Screens
{
    public class QuestScreenPresenter : Presenter
    {
        private const string QuestTargetFormat = "YOU NEED TO COLLECT {0} CANDIES OF THIS TYPE";


        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private Image questTargetImage;
        [SerializeField] private Text questTargetText;
        [SerializeField] private Button startButton;
        [SerializeField] private MusicName musicName;
        
        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private SceneNavigator sceneNavigator;
        [Inject] private BackgroundView backgroundView;
        [Inject] private MusicPlayer musicPlayer;
        [Inject] private ILevelService map;
        [Inject] private ItemSpriteMap itemSpriteMap;
        protected override void OnInit()
        {
            startButton.onClick.AddListener(OnPlayButtonClicked);
        }

        protected override void OnDispose()
        {
            startButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            screenNavigator.ChangeScreen(ScreenName.Loading);
            LoadGame();
        }

        private async UniTask LoadGame()
        {
            await sceneNavigator.OpenGame();
        } 
        
        protected override void OnShow()
        {
            musicPlayer.Play(musicName);
            backgroundView.SetSprite(backgroundImage);
            UpdateView();
        }

        private void UpdateView()
        {
            var level = map.CurrentLevel;
            questTargetImage.sprite = itemSpriteMap.GetQuestSprite(level.GoalType);
            var targetText = string.Format(QuestTargetFormat, level.GoalCount);
            questTargetText.text = targetText;
        }
    }
}