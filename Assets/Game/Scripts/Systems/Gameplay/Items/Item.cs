using UnityEngine;

namespace Game.Gameplay.Items
{
    public class Item
    {
        public Vector2Int GridPosition { get; private set; }
        public readonly ItemType ItemType;
        public Item(ItemType itemType)
        {
            ItemType = itemType;
        }

        public void SetGridPosition(Vector2Int position)
        {
            GridPosition = position;
        }
    }
}