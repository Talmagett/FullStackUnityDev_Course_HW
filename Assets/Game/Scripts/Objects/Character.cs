using Game.Components;
using UnityEngine;

namespace Game.Objects
{
    //Mediator, Facade
    public class Character : MonoBehaviour, IPushComponent, ITossComponent
    {
        [SerializeField] private HealthComponent healthComponent;
        
        [SerializeField] private LookComponent lookComponent;
        [SerializeField] private MoveComponent horizontalMoveComponent;
        
        [SerializeField] private JumpComponent jumpComponent;
        [SerializeField] private GroundedComponent groundedComponent;
       
        [SerializeField] private ForceComponent pushComponent;
        [SerializeField] private ForceComponent tossComponent;
        
        private void Awake()
        {
            lookComponent.AddCondition(healthComponent.IsAlive);
            horizontalMoveComponent.AddCondition(healthComponent.IsAlive);
            
            jumpComponent.AddCondition(healthComponent.IsAlive);
            jumpComponent.AddCondition(groundedComponent.IsGrounded);
            
            pushComponent.AddCondition(healthComponent.IsAlive);
            
            tossComponent.AddCondition(healthComponent.IsAlive);
            tossComponent.AddCondition(groundedComponent.IsGrounded);
        }

        private void OnEnable()
        {
            healthComponent.OnDead += OnHealthEmpty;
        }

        private void OnDisable()
        {
            healthComponent.OnDead -= OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            gameObject.SetActive(false);
        }

        public void Push()
        {
            pushComponent.Force(transform.right);
        }

        public void Toss()
        {
            tossComponent.Force(transform.up);
        }
    }
}