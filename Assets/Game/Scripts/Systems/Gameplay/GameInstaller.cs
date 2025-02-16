using Game.App.Map;
using Game.Gameplay.Items;
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
            MapInstaller.Install(Container);
            Container.BindInstance(itemSpriteMap).AsSingle();
            Container.BindInstance(itemView).AsSingle();
        }
    }
}