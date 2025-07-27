using Game.App.Levels;
using Game.Gameplay.Items;
using Game.Gameplay.Quests;
using Game.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "SO/New GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ItemSpriteMap itemSpriteMap;
        [SerializeField] private ItemView itemView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelService>().AsSingle().NonLazy();
            Container.Bind<Quest>().AsSingle().NonLazy();
            Container.BindInstance(itemSpriteMap).AsSingle();
            Container.BindInstance(itemView).AsSingle();
        }
    }
}