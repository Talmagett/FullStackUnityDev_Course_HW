using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Items
{
    public class ItemView : MonoBehaviour, IPoolable
    {
        [SerializeField] private SpriteRenderer itemSpriteRenderer;

        public void Reset()
        {
            itemSpriteRenderer.color=Color.white;
            transform.localScale = Vector3.one;
        }
        
        public void SetSprite(Sprite icon)
        {
            itemSpriteRenderer.sprite = icon;
        }

        //ANIMATIONS
        public UniTask MoveTo(Vector2 position, float swipeAnimationDuration)
        {
            return transform.DOLocalMove((Vector2)position, swipeAnimationDuration).ToUniTask();
        }

        public UniTask FallDown(Vector2 position, float dropSpeed)
        {
            return transform.DOLocalMove((Vector2)position, dropSpeed).SetSpeedBased().ToUniTask();
        }
        
        public UniTask FadeOut(float duration)
        {
            return itemSpriteRenderer.DOFade(0, duration).ToUniTask();
        }
        
        public UniTask FadeIn(float duration)
        {
            return itemSpriteRenderer.DOFade(0, duration).From().ToUniTask();
        }
        public UniTask Scale(Vector3 scale, float scaleDuration)
        {
            return transform.DOScale(scale, scaleDuration).ToUniTask();
        }
        
        public void OnDespawned()
        {
            
        }

        public void OnSpawned()
        {
            
        }
        
        public class Factory : PlaceholderFactory<ItemView>
        {
            
        }
        
        public class Pool : MonoMemoryPool<ItemView>
        {
            
        }

    }
}