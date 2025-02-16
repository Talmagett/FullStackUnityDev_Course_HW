using Game.Gameplay.Items;
using Game.UI.App.Level;
using Game.UI.Game;
using Game.UI.Game.Quest;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameUIInstaller : MonoInstaller
    {
        [SerializeField] private Camera camera;
        [SerializeField] private LevelNumberView levelNumberView;
        [SerializeField] private QuestView questView;
        [SerializeField] private RectTransform particleParent;
        
        public override void InstallBindings()
        {
            Container.BindInstance(camera).AsSingle();
            Container.BindInstance(levelNumberView).AsSingle();
            Container.BindInstance(questView).AsSingle();
            Container.BindInstance(particleParent).AsSingle();
            Container.BindInterfacesAndSelfTo<GameUIPresenter>().AsSingle().NonLazy();
        }
    }
}