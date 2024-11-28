using System;
using Game.Scripts.Systems.Level;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameController : IInitializable, IDisposable
    {
        public event Action<bool> OnGameOver;
        
        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;
        private readonly IDifficulty _difficulty;
        private readonly ILevelController _levelController;

        public GameController(ISnake snake, IWorldBounds worldBounds, IDifficulty difficulty, ILevelController levelController)
        {
            _snake = snake;
            _worldBounds = worldBounds;
            _difficulty = difficulty;
            _levelController = levelController;
        }

        public void Initialize()
        {
            _snake.OnSelfCollided += GameOver;
            _snake.OnMoved += OnMovedCheckIsInGameBounds;
            _levelController.OnLevelFinished += OnLevelFinished;
        }
        
        public void Dispose()
        {
            _snake.OnSelfCollided -= GameOver;
            _snake.OnMoved -= OnMovedCheckIsInGameBounds;
            _levelController.OnLevelFinished -= OnLevelFinished;
        }

        public void StartGame()
        {
            if (_difficulty.Current == 0)
                _difficulty.Next(out var d);
        }
        
        private void OnLevelFinished()
        {
            if (_difficulty.Next(out var d))
            {
                _snake.SetSpeed(_difficulty.Current);
                return;
            }

            _snake.SetActive(false);
            OnGameOver?.Invoke(true);
        }

        private void OnMovedCheckIsInGameBounds(Vector2Int position)
        {
            if (_worldBounds.IsInBounds(position))
                return;
            GameOver();
        }

        private void GameOver()
        {
            _snake.SetActive(false);
            OnGameOver?.Invoke(false);
        }
    }
}