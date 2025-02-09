using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Scripts.UI.App.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image starImage;
        [SerializeField] private Button levelButton;
        
        public event UnityAction OnLevelButtonClicked
        {
            add => levelButton.onClick.AddListener(value);
            remove => levelButton.onClick.RemoveListener(value);
        }
        public void SetStarImage(Sprite star)
        {
            starImage.sprite = star;
        }
    }
}