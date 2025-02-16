using Zenject;

namespace Game.App.Scene
{
    public sealed class SceneNavigatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.Bind<SceneNavigator>().AsSingle();
        }
    }
}