using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using UnityEngine;

namespace Game.UI.Game.Items
{
    public class ItemPresenter
    {
        public Item Item { get; }
        public ItemView ItemView { get; }
        
        public Vector2Int GridPosition => Item.GridPosition; // Получаем позицию из логики

        public ItemPresenter(Item item, ItemView itemView)
        {
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

    }
}