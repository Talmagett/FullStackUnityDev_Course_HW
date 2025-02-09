using Atomic.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class MenuScreenPresenter : Presenter
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Sprite backgroundImage;
        
        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private BackgroundView backgroundView;

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
        }

        private void OnPlayButtonClicked()
        {
            screenNavigator.ChangeScreen(ScreenName.Levels);
        }
    }
}