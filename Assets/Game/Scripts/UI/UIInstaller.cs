using Zenject;

namespace SnakeGame.UI
{
    public class UIInstaller : Installer<UIInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameUI>().FromComponentsInHierarchy().AsSingle();

            Container.BindInterfacesTo<ScorePresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DifficultyPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GameOverPresenter>().AsSingle().NonLazy();
        }
    }
}