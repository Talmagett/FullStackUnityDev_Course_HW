using SampleGame;
using UnityEngine;
using TMPro;
using Game.Scripts.Gameplay.Context;
using Atomic.Elements;
using Atomic.Presenters;

namespace Game.UI
{
    public class KillScorePresenter : Presenter
    {
        [SerializeField] private TMP_Text killScoreText;
        private IReactiveVariable<int> killScore;
        
        protected override void OnCreate()
        {
            GameContext gameContext = GameContext.Instance;
            killScore = gameContext.GetKillScore();
        }
        
        protected override void OnInit()
        {
            killScore.Observe(OnKillScoreChanged);
        }

        protected override void OnDispose()
        {
            killScore.Unsubscribe(OnKillScoreChanged);
        }
        
        private void OnKillScoreChanged(int newValue)
        {
            killScoreText.text = newValue.ToString();
        }
    }
}