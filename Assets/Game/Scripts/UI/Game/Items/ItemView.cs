using UnityEngine;
using DG.Tweening;
using Game.Common;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Game.Scripts.UI.Game.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemSpriteRenderer;
        
        public void SetSprite(Sprite icon)
        {
            itemSpriteRenderer.sprite = icon;
        }

        //ANIMATIONS
        public UniTask MoveTo(Vector2Int position, float swipeAnimationDuration)
        {
            return transform.DOLocalMove((Vector2)position, swipeAnimationDuration).ToUniTask();
        }

        public UniTask DropDown(Vector2Int position, float dropSpeed)
        {
            return transform.DOLocalMove((Vector2)position, dropSpeed).SetSpeedBased().ToUniTask();
        }
        
        public UniTask FadeOut(float duration)
        {
            return itemSpriteRenderer.DOFade(0, duration).ToUniTask();
        }
        public UniTask Scale(Vector3 scale, float scaleDuration)
        {
            return transform.DOScale(scale, scaleDuration).ToUniTask();
        }
        
        public class ItemViewPool : MonoMemoryPool<ItemView>
        {
            
        }
    }
}