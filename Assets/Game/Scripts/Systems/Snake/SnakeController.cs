using System;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame.Systems
{
    public class SnakeController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly CoinService _coinService;
        private readonly IDifficulty _difficulty;
        
        public SnakeController(ISnake snake, CoinService coinService, IDifficulty difficulty)
        {
            _snake = snake;
            _coinService = coinService;
            _difficulty = difficulty;
        }
        
        public void Initialize()
        {
            _snake.OnMoved += CheckCoinPositions;
            _coinService.OnCoinCollected+=OnCoinCollected;
            _difficulty.OnStateChanged+=OnDifficultyChanged;
        }

        public void Dispose()
        {
            _snake.OnMoved -= CheckCoinPositions;
            _coinService.OnCoinCollected-=OnCoinCollected;
            _difficulty.OnStateChanged-=OnDifficultyChanged;
        }

        private void OnDifficultyChanged()
        {
            _snake.SetSpeed(_difficulty.Current);
        }

        private void CheckCoinPositions(Vector2Int position)
        {
            _coinService.OnMovedCheckCoin(position);
        }

        private void OnCoinCollected(ICoin coin)
        {
            _snake.Expand(coin.Bones);
        }
    }
}