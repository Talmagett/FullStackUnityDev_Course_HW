using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        [SerializeField] private BulletManager bulletManager;
        [SerializeField] private InputAdapter inputAdapter;
        
        private void OnEnable()
        {
            inputAdapter.OnFire += OnFire;
        }

        private void OnDisable()
        {
            inputAdapter.OnFire -= OnFire;
        }

        private void Awake()
        {
            gameData.Player.Init(bulletManager);
        }

        private void OnFire()
        {
            gameData.Player.FireComponent.Fire(Vector3.up);
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector2(inputAdapter.MoveDirection, 0);
            gameData.Player.MoveComponent.Move(moveDirection);
        }
    }
}