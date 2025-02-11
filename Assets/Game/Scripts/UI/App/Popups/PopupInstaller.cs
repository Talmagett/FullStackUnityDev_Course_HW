using Game.Scripts.UI.Views;
using UnityEngine;
using Zenject;

namespace Game.UI
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