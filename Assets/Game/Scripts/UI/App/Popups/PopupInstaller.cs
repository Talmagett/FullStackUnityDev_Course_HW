using UnityEngine;
using Zenject;

namespace Game.UI.App.Popups
{
    public sealed class PopupInstaller : MonoInstaller
    {
        [SerializeField]
        private PopupSettingsPresenter popupSettingsPresenter;

        [SerializeField]
        private Transform popupsParent;

        public override void InstallBindings()
        {
            this.Container
                .BindInstance(popupSettingsPresenter)
                .AsSingle();
        }
    }
}