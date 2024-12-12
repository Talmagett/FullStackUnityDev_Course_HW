using System;
using Modules;

namespace SnakeGame.Systems
{
    public interface IPlayerInput
    {
        event Action<SnakeDirection> OnMoved;
    }
}