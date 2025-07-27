using Game.Gameplay.Items;
using Game.Gameplay.Match3;
using Game.UI.Game.Items;
using Game.UI.Game.Match3;
using Game.UI.Game.Quest;
using Modules.Inputs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using Match3.UI;

namespace Game.App.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private SwipeInput swipeInput;
        [SerializeField] private ItemGridView ItemGridView;
        [SerializeField] private ItemView itemView;
        [SerializeField] private QuestItemParticleView questItemParticleView;

        public override void InstallBindings()
        {
            Container.BindInstance(swipeInput).AsSingle();
            Container.BindInterfacesAndSelfTo<ItemInputPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ItemGrid>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ItemGridPresenter>().AsSingle().NonLazy();
            Container.Bind<Match3Logic>().AsSingle().NonLazy();
            Container.Bind<QuestAnimationPresenter>().AsSingle().NonLazy();
            Container.BindInstance(ItemGridView).AsSingle();
            Container.BindMemoryPool<ItemView, ItemView.Pool>().WithInitialSize(16).ExpandByDoubling().FromComponentInNewPrefab(itemView).UnderTransformGroup("ItemsPool");
            Container.BindMemoryPool<QuestItemParticleView, QuestItemParticleView.Pool>().WithInitialSize(5).ExpandByDoubling().FromComponentInNewPrefab(questItemParticleView).UnderTransformGroup("ItemParticlesPool");
        }
    }
}