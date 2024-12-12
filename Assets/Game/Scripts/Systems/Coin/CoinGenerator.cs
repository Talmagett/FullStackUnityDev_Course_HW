using System;
using Modules;
using Zenject;

namespace SnakeGame.Systems
{
    public class CoinGenerator : IInitializable, IDisposable
    {
        private readonly CoinService _coinService;
        private readonly IDifficulty _difficulty;
        
        public CoinGenerator(IDifficulty difficulty, CoinService coinService)
        {
            _difficulty = difficulty;
            _coinService = coinService;
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += SpawnCoins;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= SpawnCoins;
        }

        private void SpawnCoins()
        {
            _coinService.SpawnCoins(_difficulty.Current);
        }
    }
}