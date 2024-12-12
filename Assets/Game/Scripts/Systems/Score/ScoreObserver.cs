using System;
using Modules;
using Zenject;

namespace SnakeGame.Systems
{
    public class ScoreObserver : IInitializable, IDisposable
    {
        private readonly CoinService _coinService;
        private readonly IScore _score;
        
        public ScoreObserver(CoinService coinService, IScore score)
        {
            _coinService = coinService;
            _score = score;
        }
        
        public void Initialize()
        {
            _coinService.OnCoinCollected += OnCoinCollected;
        }

        public void Dispose()
        {
            _coinService.OnCoinCollected -= OnCoinCollected;
        }

        private void OnCoinCollected(ICoin coin)
        {
            _score.Add(coin.Score);
        }
    }
}