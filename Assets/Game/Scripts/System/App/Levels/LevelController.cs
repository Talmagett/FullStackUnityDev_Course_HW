using System;
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
        private readonly Dictionary<Vector2Int, Transform> _points = new Dictionary<Vector2Int, Transform>();
        private readonly Dictionary<Vector2Int, ItemView> _itemViews = new Dictionary<Vector2Int, ItemView>();
        private void Awake()
        {
            _currentLevel = _map.CurrentLevel;
            BuildLevel();
        }

        private void BuildLevel()
        {
            foreach (var item in _currentLevel.Field.items)
            {
                var point=new GameObject($"Point {item.point}");
                point.transform.SetParent(pointGrid);
                _points.Add(item.point,point.transform);
                var itemView = Instantiate(_itemView,(Vector2)item.point,Quaternion.identity, itemGrid);
                itemView.SetSprite(_itemSpriteMap.GetItemSprite(item.type));
                _itemViews.Add(item.point, itemView);
            }
        }
    }
}