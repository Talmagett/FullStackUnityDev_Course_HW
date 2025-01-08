using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //TODO:
            
            this.Container
                .Bind<MoneyView>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            this.Container
                .Bind<PlanetView>()
                .FromComponentsInHierarchy()
                .AsCached();
            
            this.Container
                .Bind<PlanetPopupView>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}