using Modules;
using SnakeGame.Systems;
using SnakeGame.UI;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Coin coin;
        [SerializeField] private Transform worldTransform;
        [SerializeField] private int maxDifficulty=10;

        public override void InstallBindings()
        {
            GameInstaller.Install(Container);
            DifficultyInstaller.Install(Container, maxDifficulty);
            ScoreInstaller.Install(Container);
            SnakeInstaller.Install(Container);
            UIInstaller.Install(Container);
            PlayerInstaller.Install(Container);
            CoinInstaller.Install(Container, worldTransform, coin);
        }
    }
}