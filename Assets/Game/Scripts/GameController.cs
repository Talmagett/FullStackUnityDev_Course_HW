using System;
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

        public GameController(ISnake snake, IWorldBounds worldBounds)
        {
            _snake = snake;
            _worldBounds = worldBounds;
        }

        public void Initialize()
        {
            _snake.OnSelfCollided += GameOver;
            _snake.OnMoved+=CheckWorldCollision;
        }

        public void Dispose()
        {
            _snake.OnSelfCollided -= GameOver;
            _snake.OnMoved-=CheckWorldCollision;
        }

        private void CheckWorldCollision(Vector2Int position)
        {
            if (_worldBounds.IsInBounds(position))
                return;
            GameOver();
        }

        private void GameOver()
        {
            Debug.Log("game over");
            OnGameOver?.Invoke(false);
        }
    }
}