using UnityEngine;
using DG.Tweening;
using Game.Common;
using Cysharp.Threading.Tasks;
using Zenject;
using NotImplementedException = System.NotImplementedException;

namespace Game.Scripts.UI.Game.Items
{
    public class ItemView : MonoBehaviour, IPoolable
    {
        [SerializeField] private SpriteRenderer itemSpriteRenderer;
        
        public void SetSprite(Sprite icon)
        {
            itemSpriteRenderer.sprite = icon;
        }

        //ANIMATIONS
        public UniTask MoveTo(Vector2 position, float swipeAnimationDuration)
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