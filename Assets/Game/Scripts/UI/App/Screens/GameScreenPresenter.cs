using Atomic.UI;
using Game.App;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.System.Gameplay.Quest;
using Game.Scripts.UI.App.Level;
using Game.Scripts.UI.Game.Quest;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameScreenPresenter : Presenter
    {
        [SerializeField] private LevelNumberView levelNumberView;
        [SerializeField] private QuestView questView;

        [Inject] private IMap _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        [Inject] private Quest _quest;
        private QuestPresenter _questPresenter;
        
        protected override void OnInit()
        {
            _quest.SetQuest(_map.CurrentLevel);
            _questPresenter = new QuestPresenter(questView, _quest);
            levelNumberView.SetNumberImage(_map.CurrentLevel.NumberIcon);
            questView.SetQuestTask(_itemSpriteMap.GetQuestSprite(_map.CurrentLevel.GoalType));
        }

        protected override void OnDispose()
        {
            _questPresenter.Dispose();
        }
    }
}