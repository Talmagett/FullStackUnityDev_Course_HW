using System.Collections.Generic;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.UI.Game.Items;
using UnityEngine;
using Zenject;

namespace Game.App
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private Transform itemGrid;
        [SerializeField] private Transform pointGrid;
        
        [Inject] private Map _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        [Inject] private ItemView _itemView;
        
        private LevelConfig _currentLevel;
        private Transform[,] _points;
        private ItemView[,] _itemViews;
        
        private void Awake()
        {
            _currentLevel = _map.CurrentLevel;
            BuildLevel();
        }

        private void BuildLevel()
        {
            var items = _currentLevel.Field.items;
            int width = 0;
            int height = 0;
            foreach (var item in items)
            {
                if (item.point.x > width)
                {
                    width = item.point.x;
                }
                if (item.point.y > height)
                {
                    height = item.point.y;
                }
            }

            print(width+" "+height);
            _points = new Transform[width+1,height+1];
            _itemViews = new ItemView[width+1,height+1];
            foreach (var item in items)
            {
                var point=new GameObject($"Point {item.point}");
                point.transform.SetParent(pointGrid);
                _points[item.point.x, item.point.y] = point.transform;
                
                var itemView = Instantiate(_itemView,(Vector2)item.point,Quaternion.identity, itemGrid);
                itemView.SetSprite(_itemSpriteMap.GetItemSprite(item.type));
                _itemViews[item.point.x,item.point.y] = itemView;
            }
            pointGrid.position = new Vector3(-width/2f, -height/2f, 0);
            itemGrid.position = new Vector3(-width/2f, -height/2f, 0);
        }
    }
}