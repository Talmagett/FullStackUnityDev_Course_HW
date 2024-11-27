using System;
using Modules;
using SnakeGame;
using Zenject;

namespace Game.Scripts.UI
{
    public class GameUIController : IInitializable, IDisposable
    {
        private readonly IGameUI _gameUI;
        private readonly IDifficulty _difficulty;
        private readonly IScore _score;
        
        public GameUIController(IGameUI gameUI, IDifficulty difficulty, IScore score)
        {
            _gameUI = gameUI;
            _difficulty = difficulty;
            _score = score;
        }

        public void Initialize()
        {
            _difficulty.OnStateChanged+=UpdateDifficulty;
            UpdateDifficulty();
            _score.OnStateChanged+=UpdateScore;
        }

        public void Dispose()
        {
            _difficulty.OnStateChanged-=UpdateDifficulty;
            _score.OnStateChanged-=UpdateScore;
        }

        private void UpdateScore(int scoreValue)
        {
            _gameUI.SetScore($"Score: {scoreValue}");
        }

        private void UpdateDifficulty()
        {
            _gameUI.SetDifficulty(_difficulty.Current,_difficulty.Max);
        }
    }
}