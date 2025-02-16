using Game.UI.App.Screens.Manager;
using UnityEngine;
using Zenject;

namespace Game.UI.App.Screens
{
    public sealed class ScreenInstaller : MonoInstaller
    {
        [SerializeField]
        private ScreenCatalog _screenCatalog;

        [SerializeField]
        private Transform _screenViewport;

        [SerializeField]
        private ScreenName _initialScreen;

        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<ScreenNavigator>()
                .AsSingle()
                .WithArguments(_screenCatalog, _screenViewport, _initialScreen)
                .NonLazy();
        }
    }
}