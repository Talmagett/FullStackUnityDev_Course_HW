using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.App.Level
{
    public class LevelNumberView : MonoBehaviour
    {
        [SerializeField] private Image numberImage;
        
        public void SetNumberImage(Sprite number)
        {
            numberImage.sprite = number;
        }
    }
}