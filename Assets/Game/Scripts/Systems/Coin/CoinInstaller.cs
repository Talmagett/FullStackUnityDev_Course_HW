using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame.Systems
{
    public class CoinInstaller : Installer<Transform, Coin,CoinInstaller>
    {
        private Transform _parentTransform;
        private Coin _coinPrefab;

        public CoinInstaller(Transform parentTransform, Coin coinPrefab)
        {
            _parentTransform = parentTransform;
            _coinPrefab = coinPrefab;
        }

        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<Coin, CoinPool>()
                .WithInitialSize(4)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_coinPrefab)
                .UnderTransform(_parentTransform)
                .AsSingle();

            Container
                .Bind<ICoinSpawner>()
                .To<CoinPool>()
                .FromResolve();
            
            Container
                .BindInterfacesAndSelfTo<CoinService>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<CoinGenerator>()
                .AsSingle()
                .NonLazy();
        }
    }
}