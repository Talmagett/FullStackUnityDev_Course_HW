using System;
using Components;
using ShootEmUp;
using UnityEngine;
using UnityEngine.Assertions;

namespace ShootEmUp
{
    public class Spaceship : MonoBehaviour
    {
        [field: SerializeField] public MoveComponent MoveComponent { get; private set; }
        [field: SerializeField] public HealthComponent HealthComponent { get;private set; }
        [field: SerializeField] public FireComponent FireComponent { get;private set; }
        public Vector3 Position => transform.position;

        public void Init(BulletManager bulletManager)
        {
            FireComponent.Init(bulletManager);
        }

        private void Awake()
        {
            HealthComponent.Init();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            HealthComponent.Init();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}