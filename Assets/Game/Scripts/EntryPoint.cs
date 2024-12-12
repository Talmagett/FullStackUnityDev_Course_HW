using SnakeGame.Systems;
using UnityEngine;
using Zenject;

namespace SnakeGame
{
    public class EntryPoint : MonoBehaviour
    {
        [Inject] private GameStarter _starter;
        
        public void Start()
        {
            _starter.StartGame();
        }
    }
}