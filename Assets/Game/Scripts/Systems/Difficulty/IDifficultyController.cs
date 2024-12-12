using System;

namespace SnakeGame.Systems
{
    public interface IDifficultyController
    {
        event Action OnWinGame;
    }
}