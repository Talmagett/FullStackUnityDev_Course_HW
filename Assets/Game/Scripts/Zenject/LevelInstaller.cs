using Game.Scripts.Player;
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

            Container
                .BindMemoryPool<Coin, CoinPool>()
                .WithInitialSize(4)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(coin)
                .AsSingle();
            /*
            this.Container
                .BindMemoryPool<Bullet, BulletPool>()
                // .WithFixedSize(10)
                .WithInitialSize(5)
                .WithMaxSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_bulletPrefab)
                .WithGameObjectName("Bullet")
                .UnderTransform(_worldTransform)
                .AsSingle();*/
        }
    }
}