using Atomic.UI;
using Cysharp.Threading.Tasks;
using Game.App;
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
        
        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private SceneNavigator sceneNavigator;
        [Inject] private BackgroundView backgroundView;
        
        
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
            backgroundView.SetSprite(backgroundImage);
        }
    }
}