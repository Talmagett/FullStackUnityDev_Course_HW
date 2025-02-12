using Game.Scripts.UI.Game.Items;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Common
{
    public class ItemFeatureInstaller : MonoInstaller
    {
        [SerializeField] private ItemView itemPrefab;
        [SerializeField] private Transform itemContainer;
        [Space]
        [SerializeField] private Image particlePrefab;
        [SerializeField] private RectTransform particleContainer;
        
        public override void InstallBindings()
        {
            //Container.BindInstance(itemPrefab).AsSingle();
        }
    }
}