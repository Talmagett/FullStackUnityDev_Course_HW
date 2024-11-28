using System;
using Game.Scripts.Systems.Coin;
using Modules;
using Zenject;

namespace Game.Scripts.Systems.Level
{
    public class LevelController : IInitializable, IDisposable, ILevelController
    {
        public event Action OnLevelFinished;

        private readonly CoinController _coinController;
        private readonly IDifficulty _difficulty;

        public LevelController(IDifficulty difficulty, CoinController coinController)
        {
            _difficulty = difficulty;
            _coinController = coinController;
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += SpawnCoins;
            _coinController.OnCoinsCollected += OnCoinsCollected;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged -= SpawnCoins;
            _coinController.OnCoinsCollected -= OnCoinsCollected;
        }

        private void SpawnCoins()
        {
            _coinController.SpawnCoins(_difficulty.Current);
        }

        private void OnCoinsCollected()
        {
            OnLevelFinished?.Invoke();
        }
    }
}