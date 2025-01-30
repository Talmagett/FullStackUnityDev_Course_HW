using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI.App.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private Image starImage;
        public void SetStarImage(Sprite star)
        {
            starImage.sprite = star;
        }
    }
}