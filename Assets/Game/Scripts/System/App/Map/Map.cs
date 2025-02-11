using System;
using Game.App;

namespace Game.Scripts.System.App.Map
{
    public class Map : IMap
    {
        private readonly LevelCatalog _catalog;
        public int MaxLevel => _maxLevel;
        private int _maxLevel;
        private int _currentLevel;

        public Map(LevelCatalog catalog)
        {
            _catalog = catalog;
        }
        public LevelConfig CurrentLevel { get; private set; }
        public void SetCurrentLevel(int levelConfigNumber)
        {
            CurrentLevel = _catalog.FindLevel(levelConfigNumber);
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
    }
}