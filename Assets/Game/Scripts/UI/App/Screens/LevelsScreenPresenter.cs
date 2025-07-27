using System;
using System.Collections.Generic;
using Atomic.UI;
using Game.App.Audio.Music;
using Game.App.Levels;
using Game.UI.App.Background;
using Game.UI.App.Level;
using Game.UI.App.Screens.Manager;
using UnityEngine;
using Zenject;

namespace Game.UI.App.Screens
{
    public class LevelsScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private LevelPresenter[] levelPresenters;

        [SerializeField] private MusicName musicName;
        
        [Space]
        [SerializeField] private Sprite openedLevelSprite;
        [SerializeField] private Sprite lockedLevelSprite;
        
        [SerializeField] private Sprite completedStarSprite;
        [SerializeField] private Sprite emptyStarSprite;
        
        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private BackgroundView backgroundView;
        [Inject] private LevelCatalog levelCatalog;
        [Inject] private MusicPlayer musicPlayer;
        [Inject] private ILevelService map;
        
        
        protected override void OnInit()
        {
            for (int i = 0; i < levelPresenters.Length; i++)
            {
                if (i >= levelCatalog.LevelCount) return;

                var levelConfig = levelCatalog.FindLevel(i + 1);
                levelPresenters[i].LevelConfig = levelConfig;
                levelPresenters[i].InitImages(
                    openedLevelSprite, lockedLevelSprite,
                    completedStarSprite, emptyStarSprite);
                levelPresenters[i].OnLevelSelected += LoadLevel;
                UpdateLevelViews();
            }
        }

        protected override void OnDispose()
        {
            for (int i = 0; i < levelPresenters.Length; i++)
            {
                if (i >= levelCatalog.LevelCount) return;

                levelPresenters[i].OnLevelSelected -= LoadLevel;
            }
        }

        protected override void OnShow()
        {
            musicPlayer.SetMute(false);
            backgroundView.SetSprite(backgroundImage);
            musicPlayer.Play(musicName);
            UpdateLevelViews();
        }

        private void UpdateLevelViews()
        {
            for (int i = 0; i < levelPresenters.Length; i++)
            {
                if (i >= levelCatalog.LevelCount) return;

                var levelConfig = levelCatalog.FindLevel(i + 1);
                bool isInteractable = map.MaxLevel > levelConfig.Number - 2;
                bool isCurrent = map.MaxLevel == levelConfig.Number - 1;

                levelPresenters[i].SetState(isInteractable, isCurrent);
            }
        }
        
        public void LoadLevel(LevelConfig levelConfig)
        {
            map.SetCurrentLevel(levelConfig.Number);
            screenNavigator.ChangeScreen(ScreenName.Quest);
        }
    }
}