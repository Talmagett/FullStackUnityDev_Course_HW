using Zenject;

namespace Game.Scripts.System.App.Map
{
    public class MapInstaller : Installer<MapInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<Map>().AsSingle().NonLazy();
        }
    }
}