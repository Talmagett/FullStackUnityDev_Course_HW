using System;
using Game.App;
using Game.Common;

namespace Game.Scripts.System.Gameplay.Quests
{
    public class Quest
    {
        public event Action OnQuestUpdated;
        public event Action OnQuestFinished;
        public int Target { get; private set;}
        public int Current { get; private set; }
        public ItemType GoalType { get; private set; }
        
        public void SetQuest(ItemType goalType, int goalCount)
        {
            Target = goalCount;
            Current = 0;
            GoalType = goalType;
        }
        
        public bool IsQuestTarget(ItemType itemType)=> GoalType == itemType;
        public void AddProgress()
        {
            if (Current >= Target)
                return;
            Current++;
            OnQuestUpdated?.Invoke();
            if(Current==Target)
                OnQuestFinished?.Invoke();
        }
    }
}