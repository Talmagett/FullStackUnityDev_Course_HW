using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.System.Gameplay.Match3;
using Game.System.Gameplay.Quests;
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
            Container.Bind<Quest>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ItemInputHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Match3Controller>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelGrid>().AsSingle().NonLazy();
            Container.Bind<Match3Logic>().AsSingle().NonLazy();
            Container.BindMemoryPool<ItemView,ItemView.ItemViewPool>().WithInitialSize(16).ExpandByDoubling();
        }
    }
}