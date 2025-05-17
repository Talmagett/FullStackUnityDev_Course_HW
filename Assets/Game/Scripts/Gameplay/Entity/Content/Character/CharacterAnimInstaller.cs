using Atomic.Entities;
using Game.Scripts.Gameplay.Entity.Common.TakeDamage;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CharacterAnimInstaller : SceneEntityInstaller
    {
        private const string fireEvent = "fire_event";
        
        [SerializeField]
        private string _isMovingKey = "IsMoving";
        
        [SerializeField]
        private string _takeDamageKey = "TakeDamage";
        
        [SerializeField]
        private string _deathKey = "Death";

        [SerializeField]
        private string _attack = "Attack";
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private AnimationEventReceiver _animationReceiver;

        public override void Install(IEntity entity)
        {
            entity.AddAnimator(_animator);

            entity.AddBehaviour(new MoveAnimBehaviour(_isMovingKey));
            
            entity.AddBehaviour(new FireAnimBehaviour(_attack));
            entity.AddBehaviour(new DeathAnimBehaviour(_deathKey));
            entity.GetFireRequest().Subscribe(()=>
            {
                if (!entity.GetCurrentWeapon().Value.GetFireCondition().Invoke())
                    return;
                entity.GetAnimator().SetTrigger(_attack);
            });
            _animationReceiver.Subscribe(fireEvent,
                () => entity.GetFireAction().Invoke());
            entity.AddBehaviour(new TakeDamageAnimBehaviour(_takeDamageKey));
        }
    }
}