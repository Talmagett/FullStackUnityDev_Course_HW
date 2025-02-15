using Atomic.UI;
using Cysharp.Threading.Tasks;
using Game.App;
using Game.Common;
using Game.Scripts.System.App.Map;
using Game.Scripts.UI.App.Level;
using Game.Scripts.UI.Game.Quest;
using Game.System.App.Map;
using Game.System.Gameplay.Quests;
using Game.UI.App.Level;
using Modules.Animations;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class GameScreenPresenter : Presenter
    {
        [SerializeField] private LevelNumberView levelNumberView;
        [SerializeField] private QuestView questView;
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private MusicName musicName;

        [Inject] private IMap _map;
        [Inject] private ItemSpriteMap _itemSpriteMap;
        //[Inject] private Quest _quest;
        [Inject] private BackgroundView backgroundView;
        [Inject] private MusicPlayer musicPlayer;
        [Inject] private ScreenNavigator _screenNavigator;
        [Inject] private SceneNavigator sceneNavigator;

        //private QuestPresenter _questPresenter;
        
        protected override void OnShow()
        {
            backgroundView.SetSprite(backgroundImage);
            musicPlayer.Play(musicName);
            
            levelNumberView.SetNumberImage(_map.CurrentLevel.NumberIcon);
            questView.SetQuestTask(_itemSpriteMap.GetBaseSprite(_map.CurrentLevel.GoalType));
            
            //_questPresenter = new QuestPresenter(questView, _quest);
            //_quest.OnQuestFinished+=OnQuestFinished;
        }
/*
        protected override void OnHide()
        {
            _questPresenter.Dispose();
            _quest.OnQuestFinished-=OnQuestFinished;
        }*/

        private void OnQuestFinished()
        {
            var animationQueue = new AnimationQueue();
            animationQueue.Enqueue(new DelayAnimation(2));
            animationQueue.Enqueue(new ActionAnimation(FinishLevel));
            animationQueue.Execute();
        }

        private void FinishLevel()
        {
            _screenNavigator.ChangeScreen(ScreenName.Win);
            sceneNavigator.OpenMenu();
        }
    }
}