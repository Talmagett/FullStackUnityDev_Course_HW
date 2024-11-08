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
            if (item == null)
                return false;
            CheckItemSizeValid(item.Size.x,item.Size.y);
            return CanAddItem(item,position.x,position.y);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY)
        {
            try
            {
                CheckItem(item);
                if (Contains(item))
                    return false;
                for (int x = 0; x < item.Size.x; x++)
                {
                    for (int y = 0; y < item.Size.y; y++)
                    {
                        if (_matrixItems[posX + x, posY + y] !=null)
                            return false;
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            if(item==null)
                return false;
            CheckItemSizeValid(item.Size.x,item.Size.y);
            return AddItem(item, position.x, position.y);
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            try
            {
                CheckItem(item);
                CheckPosition(posX, posY);
                if (!CanAddItem(item, posX, posY))
                    return false;
                var position=new Vector2Int(posX, posY);
                _itemsPosition.Add(item,position);
                MatrixEdit(item.Size,new Vector2Int(posX, posY),item);
                OnAdded?.Invoke(item, position);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        ///     Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (item == null)
                return false;
            
            CheckItemSizeValid(item.Size.x,item.Size.y);

            try
            {
                if (Contains(item))
                    return false;
                return FindFreePosition(item.Size, out var position);
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null)
                return false;
            CheckItemSizeValid(item.Size.x,item.Size.y);
            if (Contains(item))
                return false;
            
            try
            {
                if (FindFreePosition(item.Size, out var position))
                {
                    AddItem(item, position);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
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
            
            CheckItemSizeValid(size.x,size.y);
            
            try
            {
                for (int y = 0; y <= Height-size.y; y++)
                {
                    for (int x = 0; x <= Width-size.x; x++)
                    {
                        var canPlace = true;

                        for (int xCheck = 0; xCheck < size.x && canPlace; xCheck++)
                        {
                            for (int yCheck = 0; yCheck < size.y; yCheck++)
                            {
                                if (IsOccupied(x+xCheck, y+yCheck))
                                {
                                    canPlace = false;
                                    break;
                                }
                            }
                        }

                        if (canPlace)
                        {
                            freePosition = new Vector2Int(x, y);
                            return true;
                        }
                    }
                }
                
                freePosition = new Vector2Int();
                return false;
            }
            catch (Exception e)
            {
                freePosition = new Vector2Int();
                return false;
            }
        }

        /// <summary>
        ///     Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            if(item==null) return false;
            
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
            CheckPosition(x, y);
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
            CheckPosition(x, y);
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
            try
            {
                CheckItem(item);
                var result= _itemsPosition.Remove(item, out position);
                if (result)
                {
                    MatrixEdit(item.Size,position,null);
                    OnRemoved?.Invoke(item, position);
                }
                return result;
            }
            catch (Exception e)
            {
                position = new Vector2Int();
                return false;
            }
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
            CheckPosition(x,y);
            var item =  _matrixItems[x, y];
            if (item == null)
                throw new NullReferenceException("Item is null");
            return item;
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            return TryGetItem(position.x,position.y,out item);
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            try
            {
                item = GetItem(x, y);
                return true;
            }
            catch (Exception)
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
                throw new NullReferenceException("Item is null");
            CheckItem(item);
            
            if(!_itemsPosition.ContainsKey(item))
                throw new KeyNotFoundException("Item not found in inventory");
            
            var positions=new Vector2Int[item.Size.x*item.Size.y];
            for (int x = 0; x < item.Size.x; x++)
            {
                for (int y = 0; y < item.Size.y; y++)
                {
                    positions[x * item.Size.y + y] = new Vector2Int(_itemsPosition[item].x + x, _itemsPosition[item].y + y);
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
            catch (Exception)
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
            if (_itemsPosition.Count == 0)
                return;
            
            _itemsPosition.Clear();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _matrixItems[x, y] = null;
                }
            }
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
            CheckItem(item);
            try
            {
                CheckPosition(newPosition.x,newPosition.y);
            
                var result= _itemsPosition.Remove(item, out var oldPosition);
                if (result)
                {
                    MatrixEdit(item.Size, oldPosition, null);
                    if (oldPosition != newPosition)
                    {
                        if (CanAddItem(item, newPosition))
                        {
                            _itemsPosition.Add(item,newPosition);
                            MatrixEdit(item.Size, oldPosition, item);
                            
                            OnMoved?.Invoke(item,newPosition);
                            return true;
                        }
                    }

                    _itemsPosition.Add(item,oldPosition);
                    MatrixEdit(item.Size, oldPosition, item);
                    return false;
                }

                return false;
            }
            catch (Exception)
            {            
                return false;
            }
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
            if (_matrixItems.GetLength(0) != matrix.GetLength(0) || _matrixItems.GetLength(1) != matrix.GetLength(1))
            {
                throw new ArgumentException("Size of matrix are not equal");
            }

            for (int i = 0; i < _matrixItems.GetLength(0); i++)
            {
                for (int j = 0; j < _matrixItems.GetLength(1); j++)
                {
                    matrix[i, j] = _matrixItems[i, j];
                }
            }
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

        private void CheckItem(in Item item)
        {
            if(item == null)
                throw new ArgumentNullException("Item is null");
        }

        private void CheckItemSizeValid(in int x, in int y)
        {
            if(x<=0||y<=0||x>Width||y>Height)
                throw new ArgumentOutOfRangeException("Item size must be positive integers within the inventory bounds.");
        }
        
        private void CheckPosition(in int x, in int y)
        {
            if (x < 0 || y < 0 || x > Width || y > Height)
                throw new IndexOutOfRangeException("X and Y must be positive integers within the inventory bounds.");
        }
    }
}