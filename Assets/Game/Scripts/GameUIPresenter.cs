using System;
using SnakeGame;
using Zenject;

namespace Game.Scripts
{
    public class GameUIPresenter : IInitializable, IDisposable
    {
        private readonly GameController _gameController;
        private readonly IGameUI _gameUI;

        public GameUIPresenter(GameController gameController, IGameUI gameUI)
        {
            _gameController = gameController;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _gameController.OnGameOver+=OnGameOver;
        }

        public void Dispose()
        {
            _gameController.OnGameOver-=OnGameOver;
        }

        private void OnGameOver(bool obj)
        {
            _gameUI.GameOver(obj);
        }
    }
}