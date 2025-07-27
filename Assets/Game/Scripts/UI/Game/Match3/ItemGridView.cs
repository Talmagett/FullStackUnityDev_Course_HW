using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Match3
{
    public class ItemGridView : MonoBehaviour
    {
        [SerializeField] private float offsetY;
        [Inject] private ItemView.Pool _pool;

        public Vector2 PositionOffset { get; private set; }
        private ItemView[,] _gridItems;

        public void SetGridSize(Vector2Int size)
        {
            PositionOffset = new Vector2(-(size.x - 1) / 2f, -(size.y - 1) / 2f);
            _gridItems = new ItemView[size.x, size.y];
        }

        public ItemView SpawnItem(Vector2Int position, Sprite itemSprite, bool fromUp = false)
        {
            var itemView = _pool.Spawn();
            itemView.SetSprite(itemSprite);
            itemView.transform.SetParent(transform);
            itemView.Reset();
            itemView.transform.position = new Vector3(position.x + PositionOffset.x, position.y + PositionOffset.y + (fromUp ? offsetY : 0), 0);
            itemView.gameObject.SetActive(true);
            _gridItems[position.x, position.y] = itemView;
            return itemView;
        }

        public async UniTask Swap(Vector2Int pos1, Vector2Int pos2)
        {
            await UniTask.WhenAll(
                        _gridItems[pos1.x, pos1.y].MoveTo(pos2, 0.2f),
                        _gridItems[pos2.x, pos2.y].MoveTo(pos1, 0.2f));
            var item1 = _gridItems[pos1.x, pos1.y];
            var item2 = _gridItems[pos2.x, pos2.y];
            _gridItems[pos1.x, pos1.y] = item2;
            _gridItems[pos2.x, pos2.y] = item1;
        }

        public void DestroyItem(ItemView itemView)
        {
            itemView.gameObject.SetActive(false);
            _pool.Despawn(itemView);
        }
    }
}