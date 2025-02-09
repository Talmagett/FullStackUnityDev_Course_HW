using System;
using System.Collections.Generic;
using Atomic.UI;
using Game.App;
using Game.Scripts.UI.App.Level;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class LevelsScreenPresenter : Presenter
    {
        [SerializeField] private Sprite backgroundImage;
        [SerializeField] private LevelView[] levelView;
        
        [Inject] private ScreenNavigator screenNavigator;
        [Inject] private BackgroundView backgroundView;
        [Inject] private LevelCatalog levelCatalog;
        
        private readonly List<LevelPresenter> _levelPresenters = new();
        
        protected override void OnInit()
        {
            print(levelCatalog.LevelCount);
            for (int i = 0; i < levelView.Length; i++)
            {
                if (i >= levelCatalog.LevelCount) return;
                
                var levelConfig = levelCatalog.FindLevel(i+1);
                var presenter = new LevelPresenter(levelConfig, levelView[i], this);
                _levelPresenters.Add(presenter);
            }
        }

        protected override void OnShow()
        {
            backgroundView.SetSprite(backgroundImage);
        }

        public void LoadLevel(LevelConfig levelConfig)
        {
            screenNavigator.ChangeScreen(ScreenName.Quest);
        }
    }

    public class LevelPresenter : IDisposable
    {
        private readonly LevelConfig _levelConfig;
        private readonly LevelView _levelView;
        private readonly LevelsScreenPresenter _levelsScreenPresenter;

        public LevelPresenter(LevelConfig levelConfig, LevelView levelView, LevelsScreenPresenter levelsScreenPresenter)
        {
            this._levelConfig = levelConfig;
            this._levelView = levelView;
            this._levelsScreenPresenter = levelsScreenPresenter;
            _levelView.OnLevelButtonClicked += OnLevelButtonClicked;
        }

        public void Dispose()
        {
            _levelView.OnLevelButtonClicked -= OnLevelButtonClicked;
        }
        
        private void OnLevelButtonClicked()
        {
            _levelsScreenPresenter.LoadLevel(_levelConfig);
        }
    }
}