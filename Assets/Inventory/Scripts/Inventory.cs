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
        
        private readonly Dictionary<Item,Vector2Int> _itemsPosition = new ();
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
            if(items==null)
                throw new ArgumentNullException(nameof(items));

            foreach (var (item, position) in items)
            {
                AddItem(item, position);
            }
        }

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height)
        {
            if(items==null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height)
        {
            if(items==null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddItem(item.Key, item.Value);
            }
        }

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height)
        {
            if(items==null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        ///     Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            return CanAddItem(item,position);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY)
        {
            //return IsFree(posX, posY);
            //TODO: check for free spaces
            throw new NotImplementedException();
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
            var position=new Vector2Int(posX, posY);
            _itemsPosition.Add(item,position);
            MatrixEdit(item.Size,new Vector2Int(posX, posY),item);
            OnAdded?.Invoke(item, position);
            return true;
        }

        /// <summary>
        ///     Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if(item.Size.x<=0||item.Size.y<=0||item.Size.x>Width||item.Size.y>Height)
                throw new ArgumentOutOfRangeException(nameof(item.Size), "Item size must be positive integers within the inventory bounds.");

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            if (size.x <= 0 || size.y <= 0 || size.x > Width || size.y > Height)
            {
                freePosition = new Vector2Int();
                return false;
            }
            
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            return _itemsPosition.ContainsKey(item);
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
            return _matrixItems[x, y]!=null;
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
            return _matrixItems[x, y]==null;
        }

        /// <summary>
        ///     Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            return RemoveItem(item,out var position);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            var result= _itemsPosition.Remove(item, out position);
            if (result)
            {
                MatrixEdit(item.Size,position,null);
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
            if (x <= 0 || y <= 0 || x > Width || y > Height)
                throw new IndexOutOfRangeException("X and Y must be positive integers within the inventory bounds.");

            foreach (var (item, position) in _itemsPosition)
            {
                if (x >= position.x && y >= position.y && x <= (item.Size.x + position.x) &&
                    y <= (item.Size.y + position.y))
                {
                    return item;
                }
            }
            return null;
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            try
            {
                item = GetItem(position);
                return true;
            }
            catch (Exception e)
            {
                item = null;
                return false;
            }
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            try
            {
                item = GetItem(x, y);
                return true;
            }
            catch (Exception e)
            {
                item = null;
                return false;
            }
        }

        /// <summary>
        ///     Returns matrix positions of a specified item
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if(item == null)
                throw new ArgumentNullException("item cannot be null");

            if(!_itemsPosition.ContainsKey(item))
                throw new KeyNotFoundException("Item not found in inventory");

            var positions=new Vector2Int[item.Size.x*item.Size.y];
            for (int i = 0; i < item.Size.x; i++)
            {
                for (int j = 0; j < item.Size.y; j++)
                {
                    positions[i * item.Size.x + j] = new Vector2Int(_itemsPosition[item].x + i, _itemsPosition[item].y + j);
                }
            }
            return positions;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            try
            {
                positions = GetPositions(item);
                return true;
            }
            catch (Exception e)
            {
                positions = null;
                return false;
            }
        }

        /// <summary>
        ///     Clears all inventory items
        /// </summary>
        public void Clear()
        {
            _itemsPosition.Clear();
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
            if(item==null) throw new ArgumentNullException("item cannot be null");
            if (RemoveItem(item, out var oldPosition))
            {
                return AddItem(item, newPosition);
            }

            return false;
        }

        /// <summary>
        ///     Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Item> GetEnumerator()
            => _itemsPosition.Keys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        private void MatrixEdit(Vector2Int size, Vector2Int position, Item item)
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    _matrixItems[x + position.x, y + position.y] = item;
                }
            }
        }
    }
}