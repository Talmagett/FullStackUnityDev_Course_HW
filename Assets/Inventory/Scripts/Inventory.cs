using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable NotResolvedInText

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        private readonly Dictionary<Item, Vector2Int> _itemsPosition = new();
        private readonly Item[,] _matrixItems;

        public int Width { get; }
        public int Height { get; }
        public int Count => _itemsPosition.Count;

        public Inventory(in int width, in int height)
        {
            if (width < 1 || height < 1)
                throw new ArgumentOutOfRangeException(nameof(width), "Width and height must be positive integers.");
            Width = width;
            Height = height;
            _matrixItems = new Item[width, height];
        }

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var (item, position) in items) AddItem(item, position);
        }

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items) AddItem(item);
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items) AddItem(item.Key, item.Value);
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items) AddItem(item);
        }

        /// <summary>
        ///     Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (item == null)
                return false;
            if(!IsItemSizeValid(item.Size.x, item.Size.y))
                throw new ArgumentOutOfRangeException("Item size must be positive integers within the inventory bounds.");
            return CanAddItem(item, position.x, position.y);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY)
        {
            if (item == null)
                return false;

            if (Contains(item))
                return false;
            
            if (!IsPositionValid(posX + item.Size.x-1, posY + item.Size.y-1))
                return false;

            for (var x = 0; x < item.Size.x; x++)
            for (var y = 0; y < item.Size.y; y++)
                if (_matrixItems[posX + x, posY + y] != null)
                    return false;

            return true;
        }

        /// <summary>
        ///     Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            return AddItem(item, position.x, position.y);
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            if (item == null)
                return false;
            
            if (!IsPositionValid(posX, posY))
                return false;
            
            if(!IsItemSizeValid(item.Size.x, item.Size.y))
                throw new ArgumentOutOfRangeException("Item size must be positive integers within the inventory bounds.");

            if (!CanAddItem(item, posX, posY))
                return false;
            
            var position = new Vector2Int(posX, posY);
            _itemsPosition.Add(item, position);
            MatrixEdit(item.Size, position, item);
            OnAdded?.Invoke(item, position);
            
            return true;
        }

        /// <summary>
        ///     Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (item == null)
                return false;

            if(!IsItemSizeValid(item.Size.x, item.Size.y))
                throw new ArgumentOutOfRangeException("Item size must be positive integers within the inventory bounds.");

            if (Contains(item))
                return false;
            
            return FindFreePosition(item.Size, out var position);
        }

        /// <summary>
        ///     Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null)
                return false;
            if(!IsItemSizeValid(item.Size.x, item.Size.y))
                throw new ArgumentOutOfRangeException("Item size must be positive integers within the inventory bounds.");
            if (Contains(item))
                return false;

            
            if (FindFreePosition(item.Size, out var position))
            {
                AddItem(item, position);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            if (size.x > Width && size.y > Height)
            {
                freePosition = new Vector2Int();
                return false;
            }

            if(!IsItemSizeValid(size.x, size.y))
                throw new ArgumentOutOfRangeException("Item size must be positive integers within the inventory bounds.");

            for (var y = 0; y <= Height - size.y; y++)
            for (var x = 0; x <= Width - size.x; x++)
            {
                for (var xCheck = 0; xCheck < size.x; xCheck++)
                for (var yCheck = 0; yCheck < size.y; yCheck++)
                    if (IsOccupied(x + xCheck, y + yCheck))
                    {
                        goto FoundPosition;
                    }
                
                freePosition = new Vector2Int(x, y);
                return true;
                
                FoundPosition: ;
            }

            freePosition = new Vector2Int();
            return false;
        }

        /// <summary>
        ///     Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            return item != null && _itemsPosition.ContainsKey(item);
        }

        /// <summary>
        ///     Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            return IsOccupied(position.x, position.y);
        }

        public bool IsOccupied(in int x, in int y)
        {
            if (!IsPositionValid(x, y))
                return false;
            return _matrixItems[x, y] != null;
        }

        /// <summary>
        ///     Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            return IsFree(position.x, position.y);
        }

        public bool IsFree(in int x, in int y)
        {
            IsPositionValid(x, y);
            return _matrixItems[x, y] == null;
        }

        /// <summary>
        ///     Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            return RemoveItem(item, out var position);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            if (item == null)
            {
                position = new Vector2Int();
                return false;
            }

            var result = _itemsPosition.Remove(item, out position);
            if (result)
            {
                MatrixEdit(item.Size, position, null);
                OnRemoved?.Invoke(item, position);
            }

            return result;
        }

        /// <summary>
        ///     Returns an item at specified position
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            return GetItem(position.x, position.y);
        }

        public Item GetItem(in int x, in int y)
        {
            IsPositionValid(x, y);
            var item = _matrixItems[x, y];
            if (item == null)
                throw new NullReferenceException("Item is null");
            return item;
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            return TryGetItem(position.x, position.y, out item);
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            item = !IsPositionValid(x, y) ? null : _matrixItems[x, y];
            
            return item != null;
        }

        /// <summary>
        ///     Returns matrix positions of a specified item
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null)
                throw new NullReferenceException("Item is null");

            if (!_itemsPosition.ContainsKey(item))
                throw new KeyNotFoundException("Item not found in inventory");

            var positions = new Vector2Int[item.Size.x * item.Size.y];
            for (var x = 0; x < item.Size.x; x++)
            for (var y = 0; y < item.Size.y; y++)
                positions[x * item.Size.y + y] = new Vector2Int(_itemsPosition[item].x + x, _itemsPosition[item].y + y);

            return positions;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = null;

            if (item == null)
                return false;

            if (!_itemsPosition.ContainsKey(item))
                return false;

            positions = GetPositions(item);
            return true;
        }

        /// <summary>
        ///     Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if (_itemsPosition.Count == 0)
                return;

            _itemsPosition.Clear();
            Array.Clear(_matrixItems,0,_matrixItems.Length);
            
            OnCleared?.Invoke();
        }

        /// <summary>
        ///     Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            return _itemsPosition.Count(item => item.Key.Name == name);
        }

        /// <summary>
        ///     Moves a specified item to a target position if it exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int newPosition)
        {
            if (item == null)
                throw new ArgumentNullException("Item is null");
            
            if (!IsPositionValid(newPosition.x, newPosition.y))
                return false;

            var result = _itemsPosition.Remove(item, out var oldPosition);
            if (!result) 
                return false;
            
            if (oldPosition != newPosition)
            {
                MatrixEdit(item.Size, oldPosition, null);

                if (CanAddItem(item, newPosition))
                {
                    _itemsPosition.Add(item, newPosition);
                    MatrixEdit(item.Size, newPosition, item);

                    OnMoved?.Invoke(item, newPosition);
                    return true;
                }
            }

            _itemsPosition.Add(item, oldPosition);
            return false;
        }

        /// <summary>
        ///     Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            if (_itemsPosition.Count == 0)
                return;
            
            var items = _itemsPosition.Keys.OrderByDescending(t=>t.Size.x*t.Size.y);

            _itemsPosition.Clear();
            Array.Clear(_matrixItems,0,_matrixItems.Length);
            
            foreach (var item in items)
            {
                Debug.Log($"{item.Name}");
                if (!FindFreePosition(item.Size, out var freePosition))
                    throw new Exception("Invalid position");
                _itemsPosition.Add(item, freePosition);
                MatrixEdit(item.Size, freePosition, item);
            }
            
            /*for (var i = items.Count()- 1; i >= 0; i--)
            {
                if (!FindFreePosition(items[i].Size, out var freePosition))
                    throw new Exception("Invalid position");
                _itemsPosition.Add(items[i], freePosition);
                MatrixEdit(items[i].Size, freePosition, items[i]);
            }*/
        }

        /// <summary>
        ///     Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            if (_matrixItems.GetLength(0) != matrix.GetLength(0) || _matrixItems.GetLength(1) != matrix.GetLength(1))
                throw new ArgumentException("Size of matrix are not equal");

            Array.Copy(_matrixItems, matrix, _matrixItems.Length);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return _itemsPosition.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void MatrixEdit(Vector2Int size, Vector2Int position, Item item)
        {
            for (var x = 0; x < size.x; x++)
            for (var y = 0; y < size.y; y++)
                _matrixItems[x + position.x, y + position.y] = item;
        }

        private bool IsItemSizeValid(in int x, in int y)
        {
            return x > 0 && y > 0 && x <= Width && y <= Height;
        }

        private bool IsPositionValid(in int x, in int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }
    }
}