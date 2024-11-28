using Zenject;

namespace Game.Scripts.UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameUIController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameUIPresenter>().AsSingle().NonLazy();
        }
    }
}