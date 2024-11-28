using System;
using Modules;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class GameController : IInitializable, IDisposable, ITickable
    {
        public event Action<bool> OnGameOver;
        private readonly ISnake _snake;
        private readonly IWorldBounds _worldBounds;
        private readonly IDifficulty _difficulty;
        public GameController(ISnake snake, IWorldBounds worldBounds, IDifficulty difficulty)
        {
            _snake = snake;
            _worldBounds = worldBounds;
            _difficulty = difficulty;
        }

        public void Initialize()
        {
            _snake.OnSelfCollided += GameOver;
            _snake.OnMoved+=CheckWorldCollision;
            _difficulty.OnStateChanged += OnLevelFinishedCheck;
        }

        public void Dispose()
        {
            _snake.OnSelfCollided -= GameOver;
            _snake.OnMoved-=CheckWorldCollision;
            _difficulty.OnStateChanged -= OnLevelFinishedCheck;
        }

        private void OnLevelFinishedCheck()
        {
            if (_difficulty.Current != _difficulty.Max) return;
            
            _snake.SetActive(false);
            OnGameOver?.Invoke(true);
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
            _snake.SetActive(false);
            OnGameOver?.Invoke(false);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _difficulty.Next(out int d);
        }
    }
}