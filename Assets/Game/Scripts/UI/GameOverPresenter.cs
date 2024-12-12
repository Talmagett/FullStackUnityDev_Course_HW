using System;
using SnakeGame.Systems;
using Zenject;

namespace SnakeGame.UI
{
    public class GameOverPresenter : IInitializable, IDisposable
    {
        private readonly IGameOverObserver _gameOverObserver;
        private readonly IGameUI _gameUI;

        public GameOverPresenter(IGameOverObserver gameOverObserver, IGameUI gameUI)
        {
            _gameOverObserver = gameOverObserver;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _gameOverObserver.OnGameOver += OnGameOver;
        }

        public void Dispose()
        {
            _gameOverObserver.OnGameOver -= OnGameOver;
        }

        private void OnGameOver(bool value)
        {
            _gameUI.GameOver(value);
        }
    }
}