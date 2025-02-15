using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.App;
using Game.Scripts.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.Common
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
                //_grid[item.GridPosition.x,item.GridPosition.y] = null;
                /*if (_quest.IsQuestTarget(item.ItemType))
                {
                    tasks.Add(new QuestMoveAnimation(item, questTarget.position,_quest));
                }
                else
                {
                    item.Combinate();
                }*/
            }
            await UniTask.WhenAll(tasks);
            //_soundPlayer.Play(SoundName.Collect);
        }
    }
}