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
                .BindInterfacesAndSelfTo<PlanetPopupPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<PlanetsCatalogPresenter>()
                .AsSingle()
                .NonLazy();

            this.Container
                .Bind<PlanetPopupShower>()
                .AsSingle()
                .NonLazy();
            
            Container.BindFactory<IPlanet, Vector3, PlanetPresenter,PlanetPresenter.Factory>().AsSingle().NonLazy();
        }
    }
}