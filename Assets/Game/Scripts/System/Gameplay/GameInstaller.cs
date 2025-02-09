using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.Scripts.System.Gameplay
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "SO/New GameInstaller")]
    public class GameInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ItemSpriteMap itemSpriteMap;
        [SerializeField] private ItemView itemView;
        
        public override void InstallBindings()
        {
            MapInstaller.Install(Container);
            Container.BindInstance(itemSpriteMap).AsSingle();
            Container.BindInstance(itemView).AsSingle();
            Container.Bind<Quest.Quest>().AsSingle().NonLazy();
            // Container.Bind<Score>().AsSingle().NonLazy();
            // Container.Bind<Level>().AsSingle().NonLazy();
            // Container.Bind<LevelManager>().AsSingle().NonLazy();
        }
    }
}