using System;
using Game.App.Levels;
using Game.Gameplay.Quests;
using UnityEngine;

namespace Game.App.Map
{
    public class Map : IMap
    {
        private readonly LevelCatalog _catalog;
        public int MaxLevel => _maxLevel;
        private int _maxLevel;
        private int _currentLevel;
        private readonly Quest _quest;
        
        public Map(LevelCatalog catalog, Quest quest)
        {
            _catalog = catalog;
            _quest = quest;
        }
        
        public LevelConfig CurrentLevel { get; private set; }
        public void SetCurrentLevel(int levelConfigNumber)
        {
            CurrentLevel = _catalog.FindLevel(levelConfigNumber);
            _currentLevel = CurrentLevel.Number - 1;
            _quest.SetQuest(CurrentLevel.GoalCount,CurrentLevel.GoalType);
            Debug.Log($"Current level: {_currentLevel} : MaxLevel: {_maxLevel}");
        }

        public void OnFinishLevel()
        {
            Debug.Log($"Current level: {_currentLevel} : MaxLevel: {_maxLevel}");
            if (_currentLevel != _maxLevel) return;
            Debug.Log($"Current level: {_currentLevel} : MaxLevel: {_maxLevel}");
            _maxLevel++;
        }

        public void SetMaxLevel(int dataMaxLevel)
        {
            if (dataMaxLevel < 0)
                throw new ArgumentOutOfRangeException(nameof(dataMaxLevel));
            
            _maxLevel = dataMaxLevel;
        }
    }

    public interface IMap
    {
        int MaxLevel { get; }
        LevelConfig CurrentLevel { get; }
        void SetCurrentLevel(int levelConfigNumber);
        void OnFinishLevel();
    }
}