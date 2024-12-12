using System;
using Modules;
using UnityEngine;
using Zenject;

namespace SnakeGame.Systems
{
    public class PlayerInput : IPlayerInput, ITickable
    {
        public event Action<SnakeDirection> OnMoved;

        public void Tick()
        {
            var horizontal = (int)Input.GetAxisRaw("Horizontal");
            var vertical = (int)Input.GetAxisRaw("Vertical");
            var direction = new Vector2Int(horizontal, vertical);
            
            Move(direction);
        }

        private void Move(Vector2Int direction)
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
            OnMoved?.Invoke(snakeDirection);
        }
    }
}