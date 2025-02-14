using System;
using Game.Common;
using Game.Scripts.System.App.Map;

namespace Game.System.Gameplay.Quests
{
    public class Quest
    {
        public event Action OnQuestUpdated;
        public event Action OnQuestFinished;
        
        public int Target {get; private set;}
        public int Current {get; private set;}
        public bool IsQuestComplete() => Current >= Target;
        private ItemType GoalType { get; set; }

        public Quest(IMap map)
        {
            Target = map.CurrentLevel.GoalCount;
            Current = 0;
            GoalType = map.CurrentLevel.GoalType;
        }

        public bool IsQuestTarget(ItemType itemType)=> GoalType == itemType;
        public void AddProgress()
        {
            if (Current >= Target)
                return;
            Current++;
            OnQuestUpdated?.Invoke();
            if(IsQuestComplete())
                OnQuestFinished?.Invoke();
        }
    }
}