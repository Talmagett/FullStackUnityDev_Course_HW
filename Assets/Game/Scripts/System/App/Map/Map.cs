using System;
using Game.App;

namespace Game.Scripts.System.App.Map
{
    public class Map : IMap
    {
        public int MaxLevel => _maxLevel;
        private int _maxLevel;
        private int _currentLevel;
        public LevelConfig CurrentLevel { get; private set; }

        public void SetLevel(LevelConfig levelConfig)
        {
            CurrentLevel = levelConfig;
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
    }
}