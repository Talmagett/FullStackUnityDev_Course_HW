using System;
using System.Collections.Generic;
using Modules;
using UnityEngine;

namespace SnakeGame.Systems
{
    public class CoinService 
    {
        public event Action<ICoin> OnCoinCollected;
        public event Action OnAllCoinsCollected;

        private readonly ICoinSpawner _coinsPool;
        private readonly IWorldBounds _worldBounds;
        private readonly List<ICoin> _coins;

        public CoinService(ICoinSpawner coinsPool, IWorldBounds worldBounds)
        {
            _coins = new List<ICoin>();
            _coinsPool = coinsPool;
            _worldBounds = worldBounds;
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

        public void OnMovedCheckCoin(Vector2Int position)
        {
            for (var i = 0; i < _coins.Count; i++)
            {
                var coin = _coins[i];
                if (coin.Position != position) continue;
                
                _coinsPool.Despawn(coin as Coin);
                _coins.Remove(coin);
                OnCoinCollected?.Invoke(coin);
                break;
            }

            if (_coins.Count == 0) OnAllCoinsCollected?.Invoke();
        }
    }
}