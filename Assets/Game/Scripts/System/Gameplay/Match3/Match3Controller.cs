using System;
using Cysharp.Threading.Tasks;
using Game.App;
using Game.Common;
using Game.Scripts.UI.Game.Items;
using Modules.Animations;
using UnityEngine;
using Zenject;

namespace Game.Scripts.System.Gameplay.Match3
{
    public class Match3Controller : IInitializable, IDisposable
    {
        private readonly LevelController _levelController;
        private readonly AnimationQueue _animationQueue;
        private readonly ItemController _itemController;

        public Match3Controller(LevelController levelController, ItemController itemController)
        {
            _levelController = levelController;
            _animationQueue = new AnimationQueue();
            _itemController = itemController;
        }

        private async UniTask HandleSwipeAsync(ItemView item, Vector2Int direction)
        {
            // 1️⃣ Попытка свапа в логике
            if (!_levelController.TrySwap(item, direction, out ItemView swappedItem))
                return;

            // 2️⃣ Запуск анимации свайпа
            var swipeAnimation = new SwapAnimation(item, swappedItem);
            _animationQueue.Enqueue(swipeAnimation);
            await _animationQueue.Execute();  // Ждём окончания анимации
            return;
            /*
            // 3️⃣ Проверка матчей после свайпа
            var matches = _levelController.FindMatches();
            if (matches.Count > 0)
            {
                // 4️⃣ Запуск анимации уничтожения фишек
                var destroyAnimation = new DestroyAnimation(matches);
                _animationQueue.Enqueue(destroyAnimation);
                await _animationQueue.Execute();

                // 5️⃣ Обновление логики (удаляем из модели)
                _levelController.RemoveMatches(matches);

                // 6️⃣ Анимация падения и спауна новых фишек
                var fallAnimation = new FallAnimation(_levelController.CalculateFallPositions());
                _animationQueue.Enqueue(fallAnimation);
                await _animationQueue.Execute();

                _levelController.FillEmptySpaces();

                // 7️⃣ Повторная проверка матчей после падения
                await HandleCascade();
            }*/
        }
/*
        private async UniTask HandleCascade()
        {
            var matches = _levelController.FindMatches();
            while (matches.Count > 0)
            {
                var destroyAnimation = new DestroyAnimation(matches);
                _animationQueue.Enqueue(destroyAnimation);
                await _animationQueue.Execute();

                _levelController.RemoveMatches(matches);

                var fallAnimation = new FallAnimation(_levelController.CalculateFallPositions());
                _animationQueue.Enqueue(fallAnimation);
                await _animationQueue.Execute();

                _levelController.FillEmptySpaces();
                matches = _levelController.FindMatches();
            }
        }*/
        public void Initialize()
        {
            _itemController.OnItemSwipe += HandleSwipe;
        }

        private void HandleSwipe(ItemView arg1, Vector2Int arg2)
        {
            HandleSwipeAsync(arg1, arg2).Forget();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
