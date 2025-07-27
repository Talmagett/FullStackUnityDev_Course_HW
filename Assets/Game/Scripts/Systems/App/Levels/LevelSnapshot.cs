using System;
using Game.Gameplay.Items;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.App.Levels
{
    [Serializable]
    public struct LevelSnapshot
    {
        [SerializeField]
        public Item[] items;

        public LevelSnapshot(params Item[] items)
        {
            this.items = items;
        }

        [Serializable]
        public struct Item
        {
            [HorizontalGroup]
            public Vector2Int point;

            [HorizontalGroup]
            public ItemColor type;

            public Item(Vector2Int point, ItemColor type)
            {
                this.point = point;
                this.type = type;
            }
        }
    }
}