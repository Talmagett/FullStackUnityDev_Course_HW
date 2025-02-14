using System;
using Game.Scripts.UI.Game.Quest;
using Game.System.Gameplay.Quests;

namespace Game.UI.App.Level
{
    public class QuestPresenter : IDisposable
    {
        private readonly QuestView _questView;
        private readonly Quest _quest;
        
        public QuestPresenter(QuestView questView, Quest quest)
        {
            _questView = questView;
            _quest = quest;
            _quest.OnQuestUpdated += OnQuestUpdate;
            OnQuestUpdate();
        }

        private void OnQuestUpdate()
        {
            _questView.SetProgress($"{_quest.Current}/{_quest.Target}");
        }

        public void Dispose()
        {
            _quest.OnQuestUpdated -= OnQuestUpdate;
        }
    }
}