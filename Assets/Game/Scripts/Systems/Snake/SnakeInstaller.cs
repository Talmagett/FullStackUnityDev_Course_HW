using Modules;
using Zenject;

namespace SnakeGame.Systems
{
    public class SnakeInstaller:Installer<SnakeInstaller> 
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Snake>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesTo<SnakeController>().AsSingle().NonLazy();
        }
    }
}