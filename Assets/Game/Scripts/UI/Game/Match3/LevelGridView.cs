using System.Collections.Generic;
using Game.Scripts.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.Game.Match3
{
    public class LevelGridView : MonoBehaviour
    {
        [SerializeField] private float offsetY;

        private ItemView[,] _itemViews;
        
        [Inject] private ItemView.Pool _pool;

        private Vector2 _positionOffset;
        
       public void Inititalize(Vector2Int size)
       {
           _itemViews = new ItemView[size.x, size.y];
           _positionOffset = new Vector2(-(size.x-1) / 2f, -(size.y -1)/ 2f);
       }

       public ItemView SpawnItem(Vector2Int position, Sprite itemSprite, bool fromUp = false)
       {
           var itemView = _pool.Spawn();
           _itemViews[position.x, position.y] = itemView;
           itemView.SetSprite(itemSprite);
           itemView.transform.SetParent(transform);
           itemView.transform.position = new Vector3(position.x + _positionOffset.x, position.y + _positionOffset.y+(fromUp?offsetY:0), 0);
           return itemView;
       }
    }
}