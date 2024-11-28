using Modules;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class CoinPool : MonoMemoryPool<Vector2Int, Coin>, ICoinSpawner
    {
        ICoin ICoinSpawner.Spawn(Vector2Int position)=>
            Spawn(position);

        public void Despawn(Coin coin) => base.Despawn(coin);

        protected override void Reinitialize(Vector2Int p1, Coin item)
        {
            item.Position = p1;
            item.Generate();
        }
    }
}