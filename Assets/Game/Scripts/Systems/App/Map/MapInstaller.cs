using Game.Gameplay.Quests;
using Zenject;

namespace Game.App.Map
{
    public class MapInstaller : Installer<MapInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Map>().AsSingle().NonLazy();
            Container.Bind<Quest>().AsSingle().NonLazy();
        }
    }
}