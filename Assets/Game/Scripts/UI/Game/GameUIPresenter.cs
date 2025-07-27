using System;
using Game.App.Audio.Music;
using Game.App.Levels;
using Game.App.Scene;
using Game.Gameplay.Items;
using Game.UI.App.Background;
using Game.UI.App.Level;
using Game.UI.App.Screens.Manager;
using Game.UI.Game.Quest;
using UnityEngine;
using Zenject;

namespace Game.UI.Game
{
    public class GameUIPresenter : IInitializable, IDisposable
    {
        [Inject] private ILevelService _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        [Inject] private Gameplay.Quests.Quest _quest;
        [Inject] private LevelNumberView levelNumberView;
        [Inject] private QuestView questView;
        [Inject] private ScreenNavigator _screenNavigator;
        [Inject] private SceneNavigator sceneNavigator;
        
        private QuestPresenter _questPresenter;

        public void FinishLevel()
        {
            _screenNavigator.ChangeScreen(ScreenName.Win);
            sceneNavigator.OpenMenu();
        }

        public void Initialize()
        {
            levelNumberView.SetNumberImage(_map.CurrentLevel.NumberIcon);
            questView.SetQuestTask(_itemSpriteMap.GetBaseSprite(_map.CurrentLevel.GoalType));
            
            _questPresenter = new QuestPresenter(questView, _quest);
        }

        public void Dispose()
        {
            _questPresenter.Dispose();
        }
    }
}