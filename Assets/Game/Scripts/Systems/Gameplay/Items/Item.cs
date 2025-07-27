using UnityEngine;

namespace Game.Gameplay.Items
{
    public class Item
    {
        public Vector2Int GridPosition { get; private set; }
        public readonly ItemColor ItemType;
        public Item(ItemColor itemType)
        {
            ItemType = itemType;
        }

        public void SetGridPosition(Vector2Int position)
        {
            GridPosition = position;
        }
    }
}