using Modules;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class CoinPool : MonoMemoryPool<Vector2Int, Coin>
    {
        protected override void Reinitialize(Vector2Int p1, Coin item)
        {
            item.Position = p1;
            item.Generate();
        }
    }
}