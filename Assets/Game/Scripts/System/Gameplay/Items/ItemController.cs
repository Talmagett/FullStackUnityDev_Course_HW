using System;
using Game.App;
using Game.Scripts.UI.Game.Items;
using Modules.Inputs;
using UnityEngine;

namespace Game.Common
{
    public class ItemController : IDisposable
    {
        private readonly SwipeInput _swipeInput;
        private readonly LevelController _levelController;

        public ItemController(SwipeInput swipeInput, LevelController levelController)
        {
            _swipeInput = swipeInput;
            _levelController = levelController;
            Debug.Log($"{_swipeInput} is null");
            _swipeInput.OnSwipe += OnSwipe;
        }

        public void Dispose()
        {
            _swipeInput.OnSwipe -= OnSwipe;
        }
        
        private void OnSwipe(Vector2 startposition, Vector2 endposition, SwipeDirection direction)
        {
            var ray = Camera.main.ScreenPointToRay(startposition);
            var hit = Physics2D.Raycast(ray.origin,ray.direction);
            if (hit.transform!=null&&hit.transform.TryGetComponent(out ItemView itemView))
            {
                _levelController.TrySwap(itemView, direction.ToVector2Int());
            }
        }
    }
}