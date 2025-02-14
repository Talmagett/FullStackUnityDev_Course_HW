using System.Collections.Generic;
using Game.Common;
using Game.System.Gameplay.Quests;
using UnityEngine;

namespace Game.System.Gameplay.Match3
{
    public class Match3Logic
    {
        private readonly LevelGrid _grid;
        private readonly Quest _questTracker;

        public Match3Logic(LevelGrid grid, Quest questTracker)
        {
            _grid = grid;
            _questTracker = questTracker;
        }

        public bool TrySwap(Vector2Int pos1, Vector2Int pos2)
        {
            var item1 = _grid.GetItem(pos1);
            var item2 = _grid.GetItem(pos2);
            if (item1 == null || item2 == null) return false;

            _grid.SetItem(pos1, item2);
            _grid.SetItem(pos2, item1);

            return true;
        }
        //
        // public bool CheckMatches()
        // {
        //     // Логика поиска совпадений
        //     return false;
        // }
/*
        public void RemoveMatches(List<Item> matchedItems)
        {
            foreach (var item in matchedItems)
            {
                _grid.SetItem(item.GridPosition, null);
                _questTracker.CheckItem(item); // Отслеживаем квестовые фишки
            }
            ApplyGravity();
        }*/

        public void ApplyGravity()
        {
            // Логика падения фишек вниз
        }
    }
}