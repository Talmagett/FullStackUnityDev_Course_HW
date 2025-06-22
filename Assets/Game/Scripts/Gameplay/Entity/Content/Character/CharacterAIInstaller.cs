using Atomic.Elements;
using Atomic.Entities;
using Modules.FSM;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class CharacterAIInstaller : SceneEntityInstaller
    {
        public override void Install(IEntity entity)
        {
            entity.AddTarget(new ReactiveVariable<IEntity>());
            //Need agro
            //entity.AddBaseState(new ReactiveVariable<BaseState>());
            // BaseState idleState = new BaseState();
            // DecoratorState idleDecorator = new DecoratorState(
            //     idleState,
            //     onEnter: () => Debug.Log($"Entering Idle State for {entity}"),
            //     onExit: () => Debug.Log($"Exiting Idle State for {entity}"),
            //     onUpdate: deltaTime => Debug.Log($"Updating Idle State for {entity} with deltaTime {deltaTime}")
            // );
        }
    }
}