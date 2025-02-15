using Cysharp.Threading.Tasks;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.Common
{
    public class SwapAnimation : IAnimation
    {
        private readonly ItemPresenter _item1;
        private readonly ItemPresenter _item2;
        private const float SwipeDuration = 0.3f;

        public SwapAnimation(ItemPresenter item1, ItemPresenter item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        public async UniTask Execute()
        {
            var item1Position = _item1.ItemView.transform.position;
            var item2Position = _item2.ItemView.transform.position;
            //_item1.Swap(item1Position);
            await UniTask.WhenAll(
                _item1.ItemView.MoveTo(item2Position, SwipeDuration),
                _item2.ItemView.MoveTo(item1Position, SwipeDuration));
        }
    }
}