using Modules;
using Zenject;

namespace SnakeGame.Systems
{
    public class DifficultyInstaller : Installer<int,DifficultyInstaller>
    {
        private readonly int _maxDifficulty;
        
        public DifficultyInstaller(int maxDifficulty)
        {
            _maxDifficulty = maxDifficulty;
        }
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Difficulty>().AsSingle().WithArguments(_maxDifficulty).NonLazy();
            Container.BindInterfacesAndSelfTo<DifficultyController>().AsSingle().NonLazy();
        }
    }
}