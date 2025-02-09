using Atomic.UI;
using UnityEngine;
using Zenject;

namespace Game.UI
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