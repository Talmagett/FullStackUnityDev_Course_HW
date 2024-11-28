using System;
using System.Collections.Generic;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Systems.Coin
{
    public class CoinController : IInitializable, IDisposable
    {
        public event Action OnCoinsCollected;
        
        private readonly List<ICoin> _coins = new();
        private readonly ICoinSpawner _coinsPool;
        private readonly IScore _score;

        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;

        public CoinController(ICoinSpawner coinsPool, IScore score, ISnake snake, IWorldBounds worldBounds)
        {
            _coinsPool = coinsPool;
            _score = score;
            _snake = snake;
            _worldBounds = worldBounds;
        }

        public void Initialize()
        {
            _snake.OnMoved += CheckCoinPositions;
        }

        public void Dispose()
        {
            _snake.OnMoved -= CheckCoinPositions;
        }

        private void CheckCoinPositions(Vector2Int position)
        {
            for (var i = 0; i < _coins.Count; i++)
            {
                var coin = _coins[i];

                if (coin.Position != position) continue;

                _snake.Expand(coin.Bones);
                _score.Add(coin.Score);
                _coinsPool.Despawn(coin as Modules.Coin);
                _coins.Remove(coin);

                if (_coins.Count == 0) OnCoinsCollected?.Invoke();

                return;
            }
        }

        public void SpawnCoins(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var newPosition = _worldBounds.GetRandomPosition();
                var item = _coinsPool.Spawn(newPosition);
                _coins.Add(item);
            }
        }
    }
}