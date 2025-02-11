using UnityEngine;
using DG.Tweening;
using Game.Common;
using Cysharp.Threading.Tasks;

namespace Game.Scripts.UI.Game.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemSpriteRenderer;
        [SerializeField] private float animationDuration;
        
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
            return transform.DOLocalMove((Vector2)position, animationDuration).OnComplete(()=>SetGridPosition(position)).ToUniTask();
        }
        
        public void SetSprite(Sprite icon)
        {
            itemSpriteRenderer.sprite = icon;
        }
    }
}