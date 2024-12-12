using Zenject;

namespace SnakeGame.Systems
{
    public class ScoreInstaller : Installer<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Modules.Score>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ScoreObserver>().AsSingle().NonLazy();
        }
    }
}