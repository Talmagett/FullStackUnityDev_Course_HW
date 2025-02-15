using System.Collections.Generic;
using System.Linq;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.UI.Game.Items;
using JetBrains.Annotations;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Game.System.Gameplay.Items
{
    public class ItemRepository
    {
        private readonly Dictionary<Vector2Int, ItemPresenter> _items = new();

        public void Add(Vector2Int position, ItemPresenter presenter)
        {
            _items[position] = presenter;
        }

        public void Remove(Vector2Int position)
        {
            _items.Remove(position);
        }

        public ItemPresenter GetPresenter(Vector2Int position)
        {
            return _items[position];
            //return _items.TryGetValue(position, out var presenter) ? presenter : null;
        }

        public Item GetItem(Vector2Int position) => GetPresenter(position)?.Item;

        public Item GetItem(ItemView view)
        {
            foreach (var (key, value) in _items)
            {
                if (value.ItemView == view)
                    return value.Item;
            }

            return null;
        }

        public ItemView GetView(Vector2Int position) => GetPresenter(position)?.ItemView;
        public ItemView GetView(Item item) => GetPresenter(item.GridPosition)?.ItemView;

        public IEnumerable<ItemView> GetItemViews(HashSet<Item> matches)
        {
            return matches.Select(item => GetView(item)).ToList();
        }
    }
}