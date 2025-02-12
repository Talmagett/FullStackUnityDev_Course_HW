using UnityEngine;
using DG.Tweening;
using Game.Common;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;

namespace Game.Scripts.UI.Game.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemSpriteRenderer;
        [SerializeField] private float swipeAnimationDuration = 0.4f;
        [SerializeField] private float combinateAnimationDuration =0.3f;
        [SerializeField] private float dropSpeed=20;
        
        public Vector2Int GridPosition { get; private set; }
        public ItemType ItemType { get; private set; }

        public void SetItemType(ItemType itemType)
        {
            ItemType = itemType;
        }
        
        public void SetGridPosition(Vector2Int position)
        {
            GridPosition = position;
        }

        public UniTask MoveTo(Vector2Int position)
        {
            return transform.DOLocalMove((Vector2)position, swipeAnimationDuration).OnComplete(()=>SetGridPosition(position)).ToUniTask();
        }

        public UniTask Drop(Vector2Int position)
        {
            return transform.DOLocalMove((Vector2)position, dropSpeed).SetSpeedBased().OnComplete(()=>SetGridPosition(position)).ToUniTask();
        }
        
        public UniTask Combinate()
        {
            return UniTask.WhenAll(
                itemSpriteRenderer.DOFade(0, combinateAnimationDuration).ToUniTask(),
                transform.DOScale(Vector3.one * 1.5f, combinateAnimationDuration).OnComplete(()=>Destroy(gameObject)).ToUniTask()
                );
        }
        
        public void SetSprite(Sprite icon)
        {
            itemSpriteRenderer.sprite = icon;
        }
    }
}