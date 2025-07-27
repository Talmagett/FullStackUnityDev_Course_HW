using Cysharp.Threading.Tasks;
using Game.App.Audio.Sound;
using Game.UI.Game.Items;
using Game.UI.Game.Match3;
using Modules.Animations;
using UnityEngine;

namespace Game.UI.Game.Animations
{
    public class SwapAnimation : IAnimation
    {
        private readonly ItemGridView _itemGridView;
        private readonly SoundPlayer _soundPlayer;
        private readonly Vector2Int _item1Position;
        private readonly Vector2Int _item2Position;
        private const float SwipeDuration = 0.3f;

        public SwapAnimation(ItemGridView itemGridView, SoundPlayer soundPlayer, Vector2Int item1, Vector2Int item2)
        {
            _itemGridView = itemGridView;
            _soundPlayer = soundPlayer;
            _item1Position = item1;
            _item2Position = item2;
        }

        public async UniTask Execute()
        {
            _soundPlayer.Play(SoundName.Swap);
            await _itemGridView.Swap(_item1Position, _item2Position);
        }
    }
}