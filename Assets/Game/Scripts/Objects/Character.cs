using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    //Mediator, Facade
    public class Character : MonoBehaviour//, ShootComponent.ICondition
    {
        [SerializeField] private HealthComponent healthComponent;
        [SerializeField] private RotateComponent _rotateComponent;
        [SerializeField] private MoveComponent moveComponent;
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private DetectGroundComponent detectGroundComponent;
        
        private void Awake()
        {
            _rotateComponent.AddCondition(healthComponent.IsAlive);
            moveComponent.AddCondition(healthComponent.IsAlive);
            jumpComponent.AddCondition(healthComponent.IsAlive);
            jumpComponent.AddCondition(detectGroundComponent.IsGrounded);
        }

        private void OnEnable()
        {
            healthComponent.OnDead += OnHealthEmpty;
        }

        private void OnDisable()
        {
            healthComponent.OnDead -= OnHealthEmpty;
        }
/*
        bool ShootComponent.ICondition.Invoke()
        {
            return healthComponent.IsAlive();
        }*/

        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }
    }
}