using Cysharp.Threading.Tasks;
using Game.Scripts.UI.Game.Items;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.Common
{
    public class SwapAnimation : IAnimation
    {
        private readonly ItemView _item1;
        private readonly ItemView _item2;
        private const float SwipeDuration = 0.3f;

        public SwapAnimation(ItemView item1, ItemView item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        public async UniTask Execute()
        {
            var item1Position = _item1.transform.position;
            var item2Position = _item2.transform.position;
            //_item1.Swap(item1Position);
            await UniTask.WhenAll(
                _item1.MoveTo(item2Position, SwipeDuration),
                _item2.MoveTo(item1Position, SwipeDuration));
        }
    }
}