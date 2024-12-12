using System;
using Modules;
using Zenject;

namespace SnakeGame.UI
{
    public class ScorePresenter : IInitializable, IDisposable
    {
        private readonly IGameUI _gameUI;
        private readonly IScore _score;

        public ScorePresenter(IGameUI gameUI, IScore score)
        {
            _gameUI = gameUI;
            _score = score;
        }

        public void Initialize()
        {
            _score.OnStateChanged += UpdateScore;
            UpdateScore(_score.Current);
        }
        
        public void Dispose()
        {
            _score.OnStateChanged -= UpdateScore;
        }

        private void UpdateScore(int scoreValue)
        {
            _gameUI.SetScore(scoreValue.ToString());
        }
    }
}