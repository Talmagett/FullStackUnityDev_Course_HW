using Modules;
using UnityEngine;

namespace SnakeGame.Systems
{
    public interface ICoinSpawner
    {
        ICoin Spawn(Vector2Int position);
        void Despawn(Coin coin);
    }
}