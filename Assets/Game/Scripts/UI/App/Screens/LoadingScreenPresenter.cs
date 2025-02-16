using Atomic.UI;
using Game.UI.App.Background;
using UnityEngine;
using Zenject;

namespace Game.UI.App.Screens
{
    public class LoadingScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        [Inject] private BackgroundView backgroundView;

        protected override void OnShow()
        {
            backgroundView.SetSprite(backgroundImage);
        }
    }
}