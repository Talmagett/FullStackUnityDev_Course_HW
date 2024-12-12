using System;
using Modules;
using Zenject;

namespace SnakeGame.UI
{
    public class DifficultyPresenter : IInitializable, IDisposable
    {
        private readonly IDifficulty _difficulty;
        private readonly IGameUI _gameUI;
        
        public DifficultyPresenter(IGameUI gameUI, IDifficulty difficulty)
        {
            _gameUI = gameUI;
            _difficulty = difficulty;
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged += UpdateDifficulty;
            UpdateDifficulty();
        }
        
        public void Dispose()
        {
            _difficulty.OnStateChanged -= UpdateDifficulty;
        }

        private void UpdateDifficulty()
        {
            _gameUI.SetDifficulty(_difficulty.Current, _difficulty.Max);
        }
    }
}