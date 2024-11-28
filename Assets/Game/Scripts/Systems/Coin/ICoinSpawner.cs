using Modules;
using UnityEngine;

namespace Game.Scripts.Systems.Coin
{
    public interface ICoinSpawner
    {
        ICoin Spawn(Vector2Int position);
        void Despawn(Modules.Coin coin);
    }
}