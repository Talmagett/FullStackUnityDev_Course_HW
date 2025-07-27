using System;
using Game.Gameplay.Items;

namespace Game.Gameplay.Quests
{
    public class Quest
    {
        public event Action OnQuestUpdated;
        public event Action OnQuestFinished;

        public int Target { get; private set; }
        public int Current {get; private set;}
        public bool IsQuestComplete() => Current >= Target;
        private ItemColor GoalType { get; set; }

        public void SetQuest(int targetCount, ItemColor goalType)
        {
            Target = targetCount;
            Current = 0;
            GoalType = goalType;
        }
        
        public bool IsQuestTarget(ItemColor itemType)=> GoalType == itemType;
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