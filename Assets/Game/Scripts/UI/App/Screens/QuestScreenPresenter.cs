using Atomic.UI;
using Cysharp.Threading.Tasks;
using Game.App;
using Game.Common;
using Game.Scripts.System.App.Map;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class QuestScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private Image questTargetImage;
        [SerializeField] private Text questTargetText;
        [SerializeField] private Button startButton;
        [SerializeField] private MusicName musicName;
        
        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private SceneNavigator sceneNavigator;
        [Inject] private BackgroundView backgroundView;
        [Inject] private MusicPlayer musicPlayer;
        [Inject] private Map map;
        [Inject] private ItemSpriteMap itemSpriteMap;
        private const string QuestTargetText = "YOU NEED TO COLLECT [x] CANDIES OF THIS TYPE";
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
            screenNavigator.ChangeScreen(ScreenName.Game);
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
            var targetText =QuestTargetText.Replace("[x]", level.GoalCount.ToString());
            questTargetText.text = targetText;
        }
    }
}