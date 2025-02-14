using System.Collections.Generic;
using Game.Scripts.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Game.Match3
{
    public class LevelGridView : MonoBehaviour
    {
        [SerializeField] private float offsetY;

        private ItemView.ItemViewPool _itemViewPool;
        private ItemView[,] _itemViews;
        private Dictionary<ItemView, Vector2Int> _itemPositions=new();
        
        [Inject]
        public void Construct(ItemView.ItemViewPool itemViewPool)
        {
            _itemViewPool = itemViewPool;
        }
        
        public void SpawnItem(Vector2Int position)
        {
            var itemView = _itemViewPool.Spawn();
            _itemViews[position.x, position.y] = itemView;
            _itemPositions.Add(itemView, position);
        }

        public ItemView GetItem(Vector2Int position)
        {
            return _itemViews[position.x, position.y];
        }

        public Vector2Int GetPosition(ItemView itemView)
        {
            return _itemPositions[itemView];
        }

        public void RemoveItem(ItemView itemView)
        {
            var position = _itemPositions[itemView];
            _itemViews[position.x, position.y] = null;
            _itemPositions.Remove(itemView);
            _itemViewPool.Despawn(itemView);
        }
        
        public void RemoveItemAt(Vector2Int position)
        {
            var itemView = GetItem(position);
            RemoveItem(itemView);
        }
    }
}