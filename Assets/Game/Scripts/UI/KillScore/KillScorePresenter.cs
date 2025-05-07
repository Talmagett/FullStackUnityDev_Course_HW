using SampleGame;
using UnityEngine;
using TMPro;
using Game.Scripts.Gameplay.Context;
using Atomic.Elements;

namespace Game.UI
{
    public class KillScorePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text killScoreText;
        private IReactiveVariable<int> killScore;
        private void Awake()
        {
            GameContext gameContext = GameContext.Instance;
            killScore = gameContext.GetKillScore();
        }
        void OnEnable()
        {
            killScore.Observe(OnKillScoreChanged);
        }
        void OnDisable()
        {
            killScore.Unsubscribe(OnKillScoreChanged);
        }
        private void OnKillScoreChanged(int newValue)
        {
            killScoreText.text = newValue.ToString();
        }
    }
}