using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private Camera camera;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(camera).AsSingle();
        }
    }
}