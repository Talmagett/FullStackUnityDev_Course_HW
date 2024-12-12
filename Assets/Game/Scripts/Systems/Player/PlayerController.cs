using System;
using Modules;
using Zenject;

namespace SnakeGame.Systems
{
    public class PlayerController : IInitializable, IDisposable
    {
        private readonly IPlayerInput _playerInput;
        private readonly ISnake _snake;

        public PlayerController(ISnake snake, IPlayerInput playerInput)
        {
            _snake = snake;
            _playerInput = playerInput;
        }

        public void Initialize()
        {
            _playerInput.OnMoved += _snake.Turn;
        }

        public void Dispose()
        {
            _playerInput.OnMoved -= _snake.Turn;
        }
    }
}