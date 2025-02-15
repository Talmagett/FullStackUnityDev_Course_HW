using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using Game.Scripts.UI.Game.Match3;
using Game.System.Gameplay.Match3;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Match3
{
    public class LevelGridPresenter : IInitializable, IDisposable
    {
        private readonly LevelGrid _model;
        private readonly Match3Logic _match3Logic;
        private readonly LevelGridView _view;
        private readonly ItemSpriteMap _itemSpriteMap;

        private ItemPresenter[,] _itemPresenters;
        private readonly ItemInputHandler _itemInputHandler;
        private readonly AnimationQueue _animationQueue;

        public LevelGridPresenter(LevelGrid model, Match3Logic match3Logic, LevelGridView view, ItemSpriteMap itemSpriteMap, ItemInputHandler itemInputHandler)
        {
            _model = model;
            _match3Logic = match3Logic;
            _view = view;
            _itemSpriteMap = itemSpriteMap;
            _animationQueue = new AnimationQueue();
            _itemInputHandler = itemInputHandler;
        }
        
        public void Initialize()
        {
            _view.Inititalize(_model.GridSize);
            _itemPresenters = new ItemPresenter[_model.GridSize.x, _model.GridSize.y];
            foreach (var item in _model.GetAllItems())
            {
                var itemSprite = _itemSpriteMap.GetItemSprite(item.ItemType);
                var itemView = _view.SpawnItem(item.GridPosition, itemSprite);
                var itemPresenter = new ItemPresenter(item, itemView);
                _itemPresenters[item.GridPosition.x, item.GridPosition.y] = itemPresenter;
                //_itemPresenters.Add(item.GridPosition,itemPresenter);
            }
            _itemInputHandler.OnItemSwipe+= HandleSwipe;
        }


        public Vector2Int GetGridPosition(ItemPresenter presenter)
        {
            for (int x = 0; x < _itemPresenters.GetLength(0); x++)
            {
                for (int y = 0; y < _itemPresenters.GetLength(1); y++)
                {
                    if (_itemPresenters[x, y] == presenter)
                        return new Vector2Int(x, y);
                }
            }
            return Vector2Int.zero;
        }

        private Item GetItem(Vector2Int position) => _model.GetItem(position);

        private Item GetItem(ItemView itemView)
        {
            for (int x = 0; x < _itemPresenters.GetLength(0); x++)
            {
                for (int y = 0; y < _itemPresenters.GetLength(1); y++)
                {
                    if (_itemPresenters[x, y].ItemView == itemView)
                        return _itemPresenters[x, y].Item;
                }
            }

            return null;
        }
        
        private void HandleSwipe(ItemView itemView, Vector2Int direction)
        {
            HandleSwipeAsync(itemView, direction).Forget();
        }

        private async UniTask HandleSwipeAsync(ItemView itemView, Vector2Int direction)
        {
            if (_animationQueue.IsRunning) return;
            //if (_quest.IsQuestComplete()) return;

            var item1 = GetItem(itemView);
            var item2 = GetItem(item1.GridPosition + direction);
            if (item2 == null) return;

            if (!_match3Logic.TrySwap(item1.GridPosition, item2.GridPosition))
                return;
            var swipeAnimation =
                new SwapAnimation(_itemPresenters[item1.GridPosition.x,item1.GridPosition.y], _itemPresenters[item2.GridPosition.x,item2.GridPosition.y]);
            _animationQueue.Enqueue(swipeAnimation);
            await _animationQueue.Execute();
            
            (_itemPresenters[item1.GridPosition.x,item1.GridPosition.y], _itemPresenters[item2.GridPosition.x,item2.GridPosition.y]) = (
                _itemPresenters[item2.GridPosition.x,item2.GridPosition.y], _itemPresenters[item1.GridPosition.x,item1.GridPosition.y]);
            
/*
            var matches = FindMatches();
            while (matches.Count > 0)
            {
                var destroyAnimation = new DestroyAnimation(matches);
                _animationQueue.Enqueue(destroyAnimation);
                //_soundPlayer.Play(SoundName.Collect);
                await _animationQueue.Execute();

                _levelController.RemoveMatches(matches);
                var fallingItems = _levelController.FallDownItems();

                if (fallingItems.Count > 0)
                {
                    var fallAnimation = new FallAnimation(fallingItems);
                    _animationQueue.Enqueue(fallAnimation);
                    await _animationQueue.Execute();
                }

                _levelController.FillEmptySpaces();

                matches = _levelController.FindMatches();
            }*/
        }

        public void Dispose()
        {
            
        }
    }
}