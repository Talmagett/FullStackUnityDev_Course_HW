using System;
using UnityEngine;

namespace ShootEmUp
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        
        private void Awake()
        {
            gameData.Player.OnHealthEmpty += OnGameOver;
        }

        private void OnDestroy()
        {
            gameData.Player.OnHealthEmpty -= OnGameOver;
        }

        private void OnGameOver()
        {
            Time.timeScale = 0;
        }
    }
}