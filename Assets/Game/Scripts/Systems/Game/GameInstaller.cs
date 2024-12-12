using Zenject;

namespace SnakeGame.Systems
{
    public class GameInstaller: Installer<GameInstaller>
    {

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WorldBounds>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<GameStarter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameOverObserver>().AsSingle().NonLazy();
        }
    }
}