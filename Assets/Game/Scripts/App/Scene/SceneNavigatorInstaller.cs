using Zenject;

namespace Game.App
{
    public sealed class SceneNavigatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.Bind<SceneNavigator>().AsSingle();
        }
    }
}