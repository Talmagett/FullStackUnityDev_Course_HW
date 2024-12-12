using System;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame.Systems
{
    public class GameOverObserver : IInitializable, IDisposable, IGameOverObserver
    {
        public event Action<bool> OnGameOver;

        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;
        private readonly IDifficultyController _difficultyController;
        
        public GameOverObserver(ISnake snake, IWorldBounds worldBounds, IDifficultyController difficultyController)
        {
            _snake = snake;
            _worldBounds = worldBounds;
            _difficultyController = difficultyController;
        }
        
        public void Initialize()
        {
            _snake.OnSelfCollided += GameOver;
            _snake.OnMoved += OnMovedCheckIsInGameBounds;
            _difficultyController.OnWinGame += OnWinGame;
        }

        public void Dispose()
        {
            _snake.OnSelfCollided -= GameOver;
            _snake.OnMoved -= OnMovedCheckIsInGameBounds;
            _difficultyController.OnWinGame -= OnWinGame;
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
        
        private void OnWinGame()
        {
            _snake.SetActive(false);
            OnGameOver?.Invoke(true);
        }
    }
}