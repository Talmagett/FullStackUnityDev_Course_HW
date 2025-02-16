using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Game.Quest
{
    public class QuestItemParticleView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        public void SetImage(Sprite itemSprite)
        {
            itemImage.sprite = itemSprite;
        }
        
        public class Pool : MonoMemoryPool<QuestItemParticleView>
        {
            
        }
    }
}