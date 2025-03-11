using Game.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Objects
{
    //Mediator, Facade
    public class Character : MonoBehaviour
    {
        [SerializeField] private HealthComponent healthComponent;
        
        [SerializeField] private LookComponent lookComponent;
        [SerializeField] private MoveComponent horizontalMoveComponent;
        
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private GroundedComponent groundedComponent;
        [SerializeField] private ReloadComponent jumpReloadComponent;

        [SerializeField] private PushDetector pushDetector;
        [SerializeField] private PushDetector tossDetector;
        
        [SerializeField] private PushComponent pushComponent;
        [SerializeField] private ReloadComponent pushReloadComponent;
        
        [SerializeField] private PushComponent tossComponent;
        [SerializeField] private ReloadComponent tossReloadComponent;
        
        private void Awake()
        {
            lookComponent.AddCondition(healthComponent.IsAlive);
            horizontalMoveComponent.AddCondition(healthComponent.IsAlive);
            
            jumpComponent.AddCondition(healthComponent.IsAlive);
            jumpComponent.AddCondition(groundedComponent.IsGrounded);
            jumpComponent.AddCondition(jumpReloadComponent.IsReady);
            
            pushDetector.AddCondition(healthComponent.IsAlive);
            pushDetector.AddCondition(pushReloadComponent.IsReady);
            
            tossDetector.AddCondition(healthComponent.IsAlive);
            tossDetector.AddCondition(groundedComponent.IsGrounded);
            tossDetector.AddCondition(tossReloadComponent.IsReady);
        }

        private void OnEnable()
        {
            healthComponent.OnDead += OnHealthEmpty;
            pushComponent.OnPush += pushReloadComponent.Reload;
            tossComponent.OnPush += tossReloadComponent.Reload;
            jumpComponent.OnJump += jumpReloadComponent.Reload;
        }

        private void OnDisable()
        {
            healthComponent.OnDead -= OnHealthEmpty;
            pushComponent.OnPush -= pushReloadComponent.Reload;
            tossComponent.OnPush -= tossReloadComponent.Reload;
            jumpComponent.OnJump -= jumpReloadComponent.Reload;
        }


        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }
    }
}