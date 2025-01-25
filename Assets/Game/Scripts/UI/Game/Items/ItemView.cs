using UnityEngine;

namespace Game.Scripts.UI.Game.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer itemSpriteRenderer;

        public void SetSprite(Sprite icon)
        {
            itemSpriteRenderer.sprite = icon;
        }
    }
}