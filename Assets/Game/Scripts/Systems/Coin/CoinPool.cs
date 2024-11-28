using Modules;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Systems.Coin
{
    public class CoinPool : MonoMemoryPool<Vector2Int, Modules.Coin>, ICoinSpawner
    {
        ICoin ICoinSpawner.Spawn(Vector2Int position)
        {
            return Spawn(position);
        }

        public new void Despawn(Modules.Coin coin)
        {
            base.Despawn(coin);
        }

        protected override void Reinitialize(Vector2Int p1, Modules.Coin item)
        {
            item.Position = p1;
            item.Generate();
        }
    }
}