using Game.Scripts.Player;
using Game.Scripts.Systems.Coin;
using Game.Scripts.UI;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Zenject
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Snake snake;
        [SerializeField] private GameUI gameUI;
        [SerializeField] private WorldBounds worldBounds;
        [SerializeField] private Coin coin;
        [SerializeField] private Transform worldTransform;
        
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Score>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Difficulty>().AsSingle().WithArguments(9).NonLazy();
            Container.BindInterfacesTo<PlayerController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<Snake>().FromInstance(snake).AsSingle();
            Container.BindInterfacesAndSelfTo<GameUI>().FromInstance(gameUI).AsSingle();
            Container.BindInterfacesAndSelfTo<WorldBounds>().FromInstance(worldBounds).AsSingle();
            Container.BindInterfacesAndSelfTo<Coin>().FromInstance(coin).AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameUIController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameUIPresenter>().AsSingle().NonLazy();
            
            CoinInstaller.Install(Container,worldTransform);
        }
    }
}