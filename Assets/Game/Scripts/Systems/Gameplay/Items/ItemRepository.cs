using System.Collections.Generic;
using System.Linq;
using Game.UI.Game.Items;
using UnityEngine;

namespace Game.Gameplay.Items
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

        public void Swap(Vector2Int position1, Vector2Int position2)
        {
            (_items[position1], _items[position2]) = (_items[position2], _items[position1]);
        }
        
        public ItemPresenter GetPresenter(Vector2Int position)
        {
            return _items.GetValueOrDefault(position);
        }

        public Item GetItem(Vector2Int position) => GetPresenter(position)?.Item;

        public Item GetItem(ItemView view)
        {
            foreach (var (_, value) in _items)
            {
                if (value.ItemView == view)
                    return value.Item;
            }

            return null;
        }

        public ItemView GetView(Vector2Int position) => GetPresenter(position)?.ItemView;
        public ItemView GetView(Item item) => GetPresenter(item.GridPosition)?.ItemView;

        public IEnumerable<ItemView> GetItemViews(IEnumerable<Item> matches)
        {
            return matches.Select(GetView).ToList();
        }

        public IEnumerable<ItemPresenter> GetItems(IEnumerable<Item> fallingItems)
        {
            var presenters = new List<ItemPresenter>();
            
            foreach (var item in fallingItems)
            {
                presenters.Add(_items[item.GridPosition]);
            }

            return presenters;
        }
    }
}