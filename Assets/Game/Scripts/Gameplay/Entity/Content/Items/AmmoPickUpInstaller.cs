using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class AmmoPickUpInstaller : SceneEntityInstaller
    {
        [SerializeField] private int ammoAmount = 10;
        [SerializeField] private TriggerEventReceiver triggerEventReceiver;
        
        public override void Install(IEntity entity)
        {
            GameContext gameContext = GameContext.Instance;
            
            entity.AddGameObject(gameObject);
            entity.AddTransform(transform);
            entity.AddTriggerReceiver(triggerEventReceiver);
            
            entity.AddInteractibleTag();
            entity.AddBehaviour<ItemPickUpBehaviour>();
            entity.AddItemPickUpEvent(new BaseEvent());
            entity.AddInteractAction(new BaseAction<IEntity>(character =>
            {
                if (CurrentWeaponUseCase.AddClips(character, ammoAmount))
                {
                    //gameContext.GetEntityPool().Return(entity);
                    entity.GetItemPickUpEvent().Invoke();
                    entity.DelBehaviour<ItemPickUpBehaviour>();
                }
            }));
        }        
    }
}