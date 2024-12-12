using System;

namespace SnakeGame.Systems
{
    public interface IGameOverObserver
    {
        event Action<bool> OnGameOver;
    }
}