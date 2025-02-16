using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Items;
using Game.Gameplay.Match3;
using Game.UI.App.Screens.Manager;
using Game.UI.Game.Animations;
using Game.UI.Game.Items;
using Game.UI.Game.Quest;
using Modules.Animations;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Match3
{
    public class LevelGridPresenter : IInitializable, IDisposable
    {
        private readonly LevelGrid _levelGrid;
        private readonly Match3Logic _match3Logic;
        private readonly LevelGridView _view;
        private readonly ItemSpriteMap _itemSpriteMap;
        private readonly ItemRepository _itemRepository;
        private readonly ItemInputHandler _itemInputHandler;
        private readonly AnimationQueue _animationQueue;
        private readonly ScreenNavigator _screenNavigator;
        private readonly QuestAnimationPresenter _questAnimationPresenter;
        private readonly Gameplay.Quests.Quest _quest;
        private readonly GameUIPresenter _gameUIPresenter;
        private bool _isInteractable=true;
        
        public LevelGridPresenter(
            LevelGrid levelGrid, Match3Logic match3Logic, 
            LevelGridView view, ItemSpriteMap itemSpriteMap, 
            ItemInputHandler itemInputHandler, ScreenNavigator screenNavigator, 
            Gameplay.Quests.Quest quest, 
            QuestAnimationPresenter questAnimationPresenter, GameUIPresenter gameUIPresenter)
        {
            _itemRepository = new ItemRepository();
            _animationQueue = new AnimationQueue();
            
            _levelGrid = levelGrid;
            _match3Logic = match3Logic;
            _view = view;
            _itemSpriteMap = itemSpriteMap;
            _itemInputHandler = itemInputHandler;
            _screenNavigator = screenNavigator;
            _quest = quest;
            _questAnimationPresenter = questAnimationPresenter;
            _gameUIPresenter = gameUIPresenter;
        }

        public void Initialize()
        {
            _itemInputHandler.OnItemSwipe+= HandleSwipe;
            _view.SetGridSize(_levelGrid.GridSize);
            SpawnInitialItems(_levelGrid.GetAllItems());
            _levelGrid.OnGridChanged += SpawnItems;
            _screenNavigator.ChangeScreen(ScreenName.Game);
        }

        private void SpawnItems(IEnumerable<Item> getAllItems)
        {
            foreach (var item in getAllItems)
            {
                SpawnItem(item,true);
            }
        }

        private void SpawnInitialItems(IEnumerable<Item> getAllItems)
        {
            foreach (var item in getAllItems)
            {
                SpawnItem(item);
            }
        }
        public void Dispose()
        {
            _itemInputHandler.OnItemSwipe-= HandleSwipe;
            _levelGrid.OnGridChanged -= SpawnItems;
        }

        private async UniTask SpawnItem(Item item, bool fromUp = false)
        {
            var itemSprite = _itemSpriteMap.GetItemSprite(item.ItemType);
            var itemView = _view.SpawnItem(item.GridPosition, itemSprite,fromUp);
            var itemPresenter = new ItemPresenter(item, itemView,_view.PositionOffset);
            _itemRepository.Add(item.GridPosition,itemPresenter);
            await itemView.FadeIn(0.3f);
        }
        
        private void HandleSwipe(ItemView itemView, Vector2Int direction)
        {
            HandleSwipeAsync(itemView, direction).Forget();
        }
        
         private async UniTask HandleSwipeAsync(ItemView itemView, Vector2Int direction)
         {
             if (_animationQueue.IsRunning) return;
             if (_quest.IsQuestComplete()) return;
             if (!_isInteractable) return;
             
             var item1 = _itemRepository.GetItem(itemView);
             var item2 = _itemRepository.GetItem(item1.GridPosition + direction);
             if (item2 == null) return;
             
            SwapItems(item1, item2).Forget();
         }

         private async UniTask SwapItems(Item item1, Item item2)
         {
             if (!_match3Logic.TrySwap(item1.GridPosition, item2.GridPosition))
                 return;
             var itemView1 = _itemRepository.GetView(item1.GridPosition);
             var itemView2 = _itemRepository.GetView(item2.GridPosition);
             _isInteractable = false;

             var swapAnimation = new SwapAnimation(itemView1, itemView2);
             _animationQueue.Enqueue(swapAnimation);
             await _animationQueue.Execute();

             _itemRepository.Swap(item1.GridPosition, item2.GridPosition);
             
             await HandleMatches();
         }

         private async UniTask HandleMatches()
         {
             var matches = _match3Logic.FindMatches();
             while (matches.Count > 0)
             {
                 await DestroyMatchesAsync(matches);
                 FallItems();
                 await SpawnNewItems();
                 
                 matches = _match3Logic.FindMatches();
             }

             if (_quest.IsQuestComplete())
             {
                 await UniTask.Delay(1000);
                 _gameUIPresenter.FinishLevel();
             }
             else
                 _isInteractable = true;
         }

         private async UniTask SpawnNewItems()
         {
             var newFallingItems = _levelGrid.FillEmptySpaces(); // Получаем новые предметы

             List<ItemPresenter> viewsToAnimate = new();
             List<Vector2Int> newPositions = new();
             foreach (var item in newFallingItems)
             {
                 var itemPresenter = _itemRepository.GetPresenter(item.GridPosition);
                 viewsToAnimate.Add(itemPresenter);
                 newPositions.Add(item.GridPosition);
             }
             
             var fallAnimation = new FallAnimation(viewsToAnimate, newPositions);
             _animationQueue.Enqueue(fallAnimation);
             await _animationQueue.Execute();
         }


         private async Task FallItems()
         {
             var fallingItems = _match3Logic.FallDownItems();
             if (fallingItems.Count == 0) return;
             
             List<ItemPresenter> viewsToAnimate = new();
             List<Vector2Int> newPositions = new();
             
             foreach (var (from, to) in fallingItems)
             {
                 var itemPresenter = _itemRepository.GetPresenter(from);
                 if (itemPresenter == null) continue;

                 viewsToAnimate.Add(itemPresenter);
                 newPositions.Add(to);
             }
             
             var fallAnimation = new FallAnimation(viewsToAnimate, newPositions);
             _animationQueue.Enqueue(fallAnimation);
             //await _animationQueue.Execute();
             
             foreach (var (from, to) in fallingItems)
             {
                 var itemPresenter = _itemRepository.GetPresenter(from);
                 if (itemPresenter != null)
                 {
                     _itemRepository.Remove(from);
                     itemPresenter.UpdateGridPosition(to);
                     _itemRepository.Add(to, itemPresenter);
                 }
             }
         }
         
         private async UniTask DestroyMatchesAsync(IEnumerable<Item> matches)
         {
             var matchedItems = matches as Item[] ?? matches.ToArray();
             var itemPresenters = _itemRepository.GetItems(matchedItems);

             var questItems = _match3Logic.RemoveMatches(matchedItems);
    
             List<ItemView> itemViews = new();
             List<ItemPresenter> questItemViews = new();

             foreach (var itemPresenter in itemPresenters)
             {
                 itemViews.Add(itemPresenter.ItemView);
                 
                 if (questItems.Contains(itemPresenter.Item))
                     questItemViews.Add(itemPresenter);
             }

             var questAnimation = new QuestItemAnimation(questItemViews, _questAnimationPresenter);
             var destroyAnimation = new DestroyAnimation(itemViews);
             
             var parallelAnimation = new ParallelAnimation(questAnimation, destroyAnimation);
             _animationQueue.Enqueue(parallelAnimation);
             
             await _animationQueue.Execute();

             foreach (var itemPresenter in itemPresenters)
             {
                 _view.DestroyItem(itemPresenter.ItemView);
             }

             foreach (var item in matchedItems)
             {
                 _itemRepository.Remove(item.GridPosition);
             }
         }
    }
}