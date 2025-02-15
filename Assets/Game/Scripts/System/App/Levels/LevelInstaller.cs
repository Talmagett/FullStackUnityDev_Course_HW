using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.Scripts.UI.Game.Match3;
using Game.System.Gameplay.Match3;
using Game.System.Gameplay.Quests;
using Game.UI.Game.Match3;
using Modules.Inputs;
using UnityEngine;
using Zenject;

namespace Game.App
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private SwipeInput swipeInput;
        [SerializeField] private LevelGridView levelGridView;
        [SerializeField] private ItemView itemView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(swipeInput).AsSingle();
            Container.Bind<Quest>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ItemInputHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelGrid>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelGridPresenter>().AsSingle().NonLazy();
            Container.Bind<Match3Logic>().AsSingle().NonLazy();
            Container.BindInstance(levelGridView).AsSingle();
            Container.BindMemoryPool<ItemView, ItemView.Pool>().WithInitialSize(16).ExpandByDoubling().FromComponentInNewPrefab(itemView).UnderTransformGroup("ItemsPool");
            //Container.BindFactory<ItemView, ItemView.Factory>().FromComponentInNewPrefab(itemView);//.FromResolve();
        }
    }
}