using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        [SerializeField] private BulletManager bulletManager;
        [SerializeField] private InputManager inputManager;
        

        private void OnEnable()
        {
            inputManager.OnFire += OnFire;
        }

        private void OnDisable()
        {
            inputManager.OnFire -= OnFire;
        }

        private void OnFire()
        {
            gameData.Player.Fire(bulletManager,gameData.Player.SpaceshipBulletData.FirePoint.rotation*Vector3.up);
        }

        private void FixedUpdate()
        {
            var moveDirection = new Vector2(inputManager.MoveDirection, 0);
            gameData.Player.Move(moveDirection);
        }
    }
}