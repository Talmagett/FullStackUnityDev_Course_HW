using Modules;
using UnityEngine;

namespace Game.Scripts
{
    public interface ICoinSpawner
    {
        ICoin Spawn(Vector2Int position);
        void Despawn(Coin coin);
    }
}