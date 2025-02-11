using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NotImplementedException = System.NotImplementedException;

namespace Game.Scripts.UI.App.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image levelImage;
        [SerializeField] private Image starImage;
        [SerializeField] private Button levelButton;
        [SerializeField] private Animator bounceAnimator;
        
        public event UnityAction OnLevelButtonClicked
        {
            add => levelButton.onClick.AddListener(value);
            remove => levelButton.onClick.RemoveListener(value);
        }

        public void SetLevelImage(Sprite levelSprite)
        {
            levelImage.sprite = levelSprite;
        }
        
        public void SetStarImage(Sprite star)
        {
            starImage.sprite = star;
        }

        public void SetInteractable(bool isOpened)
        {
            levelButton.interactable = isOpened;
        }

        public void PlayBounce(bool canPlay)
        {
            bounceAnimator.enabled = canPlay;
        }
    }
}