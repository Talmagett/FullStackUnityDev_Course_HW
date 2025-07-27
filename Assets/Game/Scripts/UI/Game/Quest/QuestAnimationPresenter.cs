using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.Gameplay.Items;
using Game.UI.App.Screens;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Quest
{
    public class QuestAnimationPresenter
    {
        [Inject] private QuestItemParticleView.Pool pool;
        [Inject] private ItemSpriteMap itemSpriteMap;
        [Inject] private Camera camera;
        [Inject] private Gameplay.Quests.Quest quest;
        [Inject] private QuestView questView;
        [Inject] private RectTransform particleParent;
        
        public async UniTask CreateItem(ItemColor type, Vector2 position)
        {
            var itemParticle = pool.Spawn();
            var pos = camera.WorldToScreenPoint(position);
            itemParticle.transform.SetParent(particleParent);
            itemParticle.transform.position = pos;
            itemParticle.SetImage(itemSpriteMap.GetItemSprite(type));
            itemParticle.transform.localScale=Vector3.one;
            var randDur = Random.Range(0.4f, 0.7f);
            await itemParticle.transform.DOMove(questView.Position, randDur);
            questView.Bounce();
            quest.AddProgress();
            pool.Despawn(itemParticle);
        }
    }
}