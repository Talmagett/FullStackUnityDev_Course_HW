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
                .AsSingle()
                .NonLazy();
        }
    }
}