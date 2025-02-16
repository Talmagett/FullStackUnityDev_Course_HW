using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UI.Game.Items;
using Game.UI.Game.Match3;
using Modules.Animations;
using UnityEngine;

namespace Game.UI.Game.Animations
{
    public class DestroyAnimation : IAnimation
    {
        private readonly IEnumerable<ItemView> _matches;
        private const float FadeDuration = 0.3f;
        private const float ScaleDuration = 0.3f;
        public DestroyAnimation(IEnumerable<ItemView> matches)
        {
            _matches = matches;
        }

        public async UniTask Execute()
        {
            var tasks = new List<UniTask>();
            foreach (var item in _matches)
            {
                tasks.Add(item.FadeOut(FadeDuration));
                tasks.Add(item.Scale(Vector3.one*1.5f,ScaleDuration));
            }
            await UniTask.WhenAll(tasks);
            //_soundPlayer.Play(SoundName.Collect);
        }
    }
}