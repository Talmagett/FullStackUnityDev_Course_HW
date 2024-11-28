using System;
using Game.Scripts.Player;
using Modules;
using UnityEngine;
using Zenject;

namespace Game.Scripts
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
            _playerInput.OnMoved += OnMoved;
        }

        public void Dispose()
        {
            _playerInput.OnMoved -= OnMoved;
        }

        private void OnMoved(Vector2Int direction)
        {
            var snakeDirection = SnakeDirection.NONE;
            if (direction.x == 1)
                snakeDirection = SnakeDirection.RIGHT;
            else if (direction.x == -1)
                snakeDirection = SnakeDirection.LEFT;
            else if (direction.y == 1)
                snakeDirection = SnakeDirection.UP;
            else if (direction.y == -1)
                snakeDirection = SnakeDirection.DOWN;
            _snake.Turn(snakeDirection);
        }
    }
}