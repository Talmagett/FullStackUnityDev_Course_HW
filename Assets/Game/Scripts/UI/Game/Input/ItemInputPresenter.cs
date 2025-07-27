using System;
using Game.UI.Game.Items;
using Modules.Inputs;
using UnityEngine;

namespace Match3.UI
{
    public class ItemInputPresenter : IDisposable
    {
        private readonly SwipeInput _swipeInput;
        public event Action<ItemView, Vector2Int> OnItemSwipe;
        public ItemInputPresenter(SwipeInput swipeInput)
        {
            _swipeInput = swipeInput;
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
                OnItemSwipe?.Invoke(itemView, direction.ToVector2Int());
            }
        }
    }
}