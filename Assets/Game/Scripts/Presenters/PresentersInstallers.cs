using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            //TODO:
            
            this.Container
                .BindInterfacesTo<MoneyPresenter>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesTo<PlanetsCatalogPresenter>()
                .AsSingle()
                .NonLazy();
            /*
            this.Container
                .BindInterfacesTo<PlanetPopupPresenter>()
                .AsSingle()
                .NonLazy();*/
        }
    }
}