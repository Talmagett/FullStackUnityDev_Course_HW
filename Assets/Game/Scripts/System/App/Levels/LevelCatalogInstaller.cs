using UnityEngine;
using Zenject;

namespace Game.App
{
    [CreateAssetMenu(
        fileName = "LevelCatalogInstaller",
        menuName = "Levels/New LevelCatalogInstaller"
    )]
    public class LevelCatalogInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelCatalog levelCatalog;
        
        public override void InstallBindings()
        {
            Container.BindInstance(levelCatalog).AsSingle();
        }
    }
}