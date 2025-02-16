using Cysharp.Threading.Tasks;
using Game.Common;
using UnityEngine;

namespace Game.UI.Game.Items
{
    public class ItemPresenter
    {
        private readonly Vector2 _positionOffset;
        public Item Item { get; }
        public ItemView ItemView { get; }
        
        public Vector2Int GridPosition => Item.GridPosition;

        public ItemPresenter(Item item, ItemView itemView, Vector2 positionOffset)
        {
            _positionOffset = positionOffset;
            Item = item;
            ItemView = itemView;
        }

        public async UniTask Swap(Vector2Int targetPosition)
        {
            await ItemView.MoveTo(targetPosition, 0.2f);
            Item.SetGridPosition(targetPosition);
        }
/*
        public async UniTask DestroyItem()
        {
            await ItemView.PlayDestroyAnimation();
        }*/

        public void UpdateGridPosition(Vector2Int pos2)
        {
    
        }

        public async UniTask FallDown(float dropSpeed)
        {
            await ItemView.FallDown(Item.GridPosition+_positionOffset,dropSpeed);
        }
    }
}