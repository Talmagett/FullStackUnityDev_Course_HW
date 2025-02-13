using Game.Common;
using Game.Scripts.System.Gameplay.Match3;
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
            Container.BindInterfacesAndSelfTo<ItemController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Match3Controller>().AsSingle().NonLazy();
        }
    }
}