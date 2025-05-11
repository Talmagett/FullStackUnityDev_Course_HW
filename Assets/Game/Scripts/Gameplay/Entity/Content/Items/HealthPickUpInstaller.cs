using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class HealthPickUpInstaller : SceneEntityInstaller
    {
        [SerializeField] private int healAmount = 1;
        [SerializeField] private TriggerEventReceiver triggerEventReceiver;
        
        public override void Install(IEntity entity)
        {
            GameContext gameContext = GameContext.Instance;

            entity.AddGameObject(gameObject);
            entity.AddTransform(transform);
            entity.AddTriggerReceiver(triggerEventReceiver);
            
            entity.AddInteractibleTag();
            entity.AddItemPickUpEvent(new BaseEvent());
            entity.AddBehaviour<ItemPickUpBehaviour>();
            entity.AddInteractAction(new BaseAction<IEntity>(character => 
                 {
                    if(HealthUseCase.Heal(character, healAmount))
                    {
                        //{gameContext.GetEntityPool().Return(entity);
                        entity.GetItemPickUpEvent().Invoke();
                        entity.DelInteractibleTag();
                    }
                 }));
        }
    }
}