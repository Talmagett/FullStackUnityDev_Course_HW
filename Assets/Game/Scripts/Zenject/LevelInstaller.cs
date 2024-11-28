using Game.Scripts.Player;
using Game.Scripts.Systems.Coin;
using Game.Scripts.Systems.Level;
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
        [SerializeField] private int maxDifficulty=10;
        
        public override void InstallBindings()
        {
            //Data
            Container.BindInterfacesAndSelfTo<Snake>().FromInstance(snake).AsSingle();
            Container.BindInterfacesAndSelfTo<GameUI>().FromInstance(gameUI).AsSingle();
            Container.BindInterfacesAndSelfTo<WorldBounds>().FromInstance(worldBounds).AsSingle();
            Container.BindInterfacesAndSelfTo<Coin>().FromInstance(coin).AsSingle();
            
            //Systems
            Container.BindInterfacesAndSelfTo<Score>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Difficulty>().AsSingle().WithArguments(maxDifficulty).NonLazy();
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();

            UIInstaller.Install(Container);
            PlayerInstaller.Install(Container);
            CoinInstaller.Install(Container, worldTransform);
        }

        public override void Start()
        {
            Container.Resolve<GameController>().StartGame();
        }
    }
}