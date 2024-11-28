using UnityEngine;
using Zenject;

namespace Game.Scripts.Systems.Coin
{
    public class CoinInstaller : Installer<Transform, CoinInstaller>
    {
        [Inject] protected Modules.Coin _coinPrefab;

        [Inject] protected Transform _worldTransform;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<CoinController>()
                .AsSingle()
                .NonLazy();
            Container
                .BindMemoryPool<Modules.Coin, CoinPool>()
                .WithInitialSize(4)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_coinPrefab)
                .AsSingle();

            Container
                .Bind<ICoinSpawner>()
                .To<CoinPool>()
                .FromResolve();
        }
    }
}