using Game.Views;
using Modules.Planets;
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
            this.Container
                .BindInterfacesAndSelfTo<MoneyPresenter>()
                .AsSingle()
                .NonLazy();
            
            this.Container
                .BindInterfacesTo<PlanetPopupPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesTo<PlanetsCatalogPresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindFactory<IPlanet,PlanetView,PlanetPresenter,PlanetPresenter.Factory>().AsSingle().NonLazy();
        }
    }
}