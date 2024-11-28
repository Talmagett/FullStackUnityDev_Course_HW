using System;
using System.Collections.Generic;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class LevelController : IInitializable, IDisposable
    {
        private readonly ISnake _snake;
        private readonly IDifficulty _difficulty;
        private List<ICoin> _coins;
        private readonly ICoinSpawner _coinsPool;
        private readonly IWorldBounds _worldBounds;
        private readonly IScore _score;

        public LevelController(ISnake snake, IDifficulty difficulty, ICoinSpawner coinsPool, IWorldBounds worldBounds, IScore score)
        {
            _snake = snake;
            _difficulty = difficulty;
            _coinsPool = coinsPool;
            _worldBounds = worldBounds;
            _score = score;
        }

        public void Initialize()
        {
            _snake.OnMoved += CheckCoinPositions;
            _difficulty.OnStateChanged += SpawnCoins;
            _difficulty.Next(out int difficulty);
        }

        public void Dispose()
        {
            _snake.OnMoved -= CheckCoinPositions;
            _difficulty.OnStateChanged -= SpawnCoins;
        }
        
        private void CheckCoinPositions(Vector2Int position)
        {
            foreach (var coin in _coins)
            {
                if(coin.Position!=position) continue;

                _snake.Expand(coin.Bones);
                _score.Add(coin.Score);
                _coinsPool.Despawn(coin as Coin);
                _coins.Remove(coin);
                
                if (_coins.Count == 0)
                {
                    _difficulty.Next(out int difficulty);
                }
                return;
            }
        }
        
        private void SpawnCoins()
        {
            var difficulty = _difficulty.Current;
            _coins = new List<ICoin>(difficulty);
            _snake.SetSpeed(difficulty);
            for (int i = 0; i < difficulty; i++)
            {
                var newPosition = _worldBounds.GetRandomPosition();
                var item = _coinsPool.Spawn(newPosition);
                _coins.Add(item);
            }
        }
    }
}