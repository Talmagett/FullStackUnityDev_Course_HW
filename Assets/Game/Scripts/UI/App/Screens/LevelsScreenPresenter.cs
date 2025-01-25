using UnityEngine;

namespace Game.UI
{
    public class LevelsScreenPresenter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer backgroundSpriteRenderer;

        public void SetSprite(Sprite sprite)
        {
            backgroundSpriteRenderer.sprite = sprite;
        }
    }
}