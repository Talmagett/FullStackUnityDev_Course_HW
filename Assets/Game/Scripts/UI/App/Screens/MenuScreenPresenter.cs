using Atomic.UI;
using Cysharp.Threading.Tasks;
using Game.App;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class MenuScreenPresenter : Presenter
    {
        [SerializeField] private Button playButton;
        [Inject] private SceneNavigator sceneNavigator;

        protected override void OnInit()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        protected override void OnDispose()
        {
            playButton.onClick.RemoveListener(OnPlayButtonClicked);
        }

        private void OnPlayButtonClicked()
        {
            sceneNavigator.OpenGame().Forget();
            Hide();
        }
    }
}