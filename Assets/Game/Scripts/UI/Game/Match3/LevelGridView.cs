using Game.Gameplay.Items;
using Game.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.UI.Game.Match3
{
    public class LevelGridView : MonoBehaviour
    {
        [SerializeField] private float offsetY;
        [Inject] private ItemView.Pool _pool;
        
        public Vector2 PositionOffset { get; private set; }

        public void SetGridSize(Vector2Int size)
       {
           PositionOffset = new Vector2(-(size.x-1) / 2f, -(size.y -1)/ 2f);
       }

       public ItemView SpawnItem(Vector2Int position, Sprite itemSprite, bool fromUp = false)
       {
           var itemView = _pool.Spawn();
           itemView.SetSprite(itemSprite);
           itemView.transform.SetParent(transform);
           itemView.Reset();
           itemView.transform.position = new Vector3(position.x + PositionOffset.x, position.y + PositionOffset.y+(fromUp?offsetY:0), 0);
           itemView.gameObject.SetActive(true);
           return itemView;
       }

       public void DestroyItem(ItemView itemView)
       {
           itemView.gameObject.SetActive(false);
           _pool.Despawn(itemView);
       }
    }
}