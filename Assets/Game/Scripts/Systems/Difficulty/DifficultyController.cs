using System;
using Modules;
using Zenject;

namespace SnakeGame.Systems
{
    public class DifficultyController : IInitializable, IDisposable, IDifficultyController
    {
        public event Action OnWinGame;
        
        private readonly IDifficulty _difficulty;
        private readonly CoinService _coinService;
        
        public DifficultyController(IDifficulty difficulty, CoinService coinService)
        {
            _difficulty = difficulty;
            _coinService = coinService;
        }
        
        public void Initialize()
        {
            _coinService.OnAllCoinsCollected += NextLevel;
        }
        
        public void Dispose()
        {
            _coinService.OnAllCoinsCollected -= NextLevel;
        }

        private void NextLevel()
        {
            if (_difficulty.Next(out _)) return;
            
            OnWinGame?.Invoke();
        }
    }
}