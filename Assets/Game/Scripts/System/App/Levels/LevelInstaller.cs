using Game.Common;
using Modules.Inputs;
using UnityEngine;
using Zenject;

namespace Game.App
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private SwipeInput swipeInput;
        [SerializeField] private LevelController levelController;
        
        public override void InstallBindings()
        {
            Container.BindInstance(swipeInput).AsSingle();
            Container.BindInstance(levelController).AsSingle();
            Container.BindInterfacesTo<ItemController>().AsSingle().NonLazy();
        }
    }
}