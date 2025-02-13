using Cysharp.Threading.Tasks;
using Game.Scripts.UI.Game.Items;
using Modules.Animations;

namespace Game.Common
{
    public class SwapAnimation : IAnimation
    {
        private readonly ItemView _item1;
        private readonly ItemView _item2;

        public SwapAnimation(ItemView item1, ItemView item2)
        {
            _item1 = item1;
            _item2 = item2;
        }

        public async UniTask Execute()
        {
            await UniTask.WhenAll(
                _item1.MoveTo(_item2.GridPosition),
                _item2.MoveTo(_item1.GridPosition));
        }
    }
}