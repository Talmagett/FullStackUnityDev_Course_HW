using System;
using Game.App;

namespace Game.Scripts.System.Gameplay.Quest
{
    public class Quest
    {
        public event Action OnQuestUpdated;
        public int Target { get; private set;}
        public int Current { get; private set; }

        private LevelConfig _levelConfig;
        public void SetQuest(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            Target = levelConfig.GoalCount;
            Current = 0;
        }
        
        public void AddProgress()
        {
            Current++;
            OnQuestUpdated?.Invoke();
        }
    }
}