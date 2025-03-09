using Game.Components;
using SampleGame;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Objects
{
    //Mediator, Facade
    public class Character : MonoBehaviour//, ShootComponent.ICondition
    {
        [SerializeField] private HealthComponent healthComponent;
        
        [SerializeField] private RotateComponent rotateComponent;
        [SerializeField] private MoveComponent moveComponent;
        
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private GroundedComponent groundedComponent;
        [SerializeField] private ReloadComponent jumpReloadComponent;
        
        [SerializeField] private PushComponent pushComponent;
        [SerializeField] private ReloadComponent pushReloadComponent;
        
        [SerializeField] private TossComponent tossComponent;
        [SerializeField] private ReloadComponent tossReloadComponent;
        
        private void Awake()
        {
            rotateComponent.AddCondition(healthComponent.IsAlive);
            moveComponent.AddCondition(healthComponent.IsAlive);
            
            jumpComponent.AddCondition(healthComponent.IsAlive);
            jumpComponent.AddCondition(groundedComponent.IsGrounded);
            jumpComponent.AddCondition(jumpReloadComponent.IsReady);
            
            pushComponent.AddCondition(healthComponent.IsAlive);
            pushComponent.AddCondition(pushReloadComponent.IsReady);
            
            tossComponent.AddCondition(healthComponent.IsAlive);
            tossComponent.AddCondition(groundedComponent.IsGrounded);
            tossComponent.AddCondition(tossReloadComponent.IsReady);
        }

        private void OnEnable()
        {
            healthComponent.OnDead += OnHealthEmpty;
            pushComponent.OnPush += pushReloadComponent.Reload;
            jumpComponent.OnJump += jumpReloadComponent.Reload;
        }

        private void OnDisable()
        {
            healthComponent.OnDead -= OnHealthEmpty;
            pushComponent.OnPush -= pushReloadComponent.Reload;
            jumpComponent.OnJump -= jumpReloadComponent.Reload;
        }


        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }
    }
}