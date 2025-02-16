using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.System.Gameplay.Quests;
using Game.UI.Game.Items;
using Modules.Animations;
using UnityEngine;

namespace Game.Common
{
    public class QuestMoveAnimation : IAnimation
    {
        private readonly ItemView view;
        private readonly Vector3 targetPosition;
        private readonly Quest quest;

        public QuestMoveAnimation(ItemView view, Vector3 targetPosition, Quest quest)
        {
            this.view = view;
            this.targetPosition = targetPosition;
            this.quest = quest;
        }
        public async UniTask Execute()
        {
            await view.transform.DOMove(targetPosition, Random.Range(0.4f, 1.5f));
            await view.transform.DOPunchScale(Vector3.one*0.5f, 0.3f);
            quest.AddProgress();
            Object.Destroy(view.gameObject);
        }
    }
}