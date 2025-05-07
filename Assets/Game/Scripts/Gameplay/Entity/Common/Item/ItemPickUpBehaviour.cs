using System;
using Atomic.Elements;
using Atomic.Entities;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class ItemPickUpBehaviour : IEntityInit, IEntityDispose
    {
        private IAction<IEntity> _onPickUpAction;
        private TriggerEventReceiver _triggerEventReceiver;

        public void Init(in IEntity entity)
        {
            _triggerEventReceiver = entity.GetTriggerReceiver();
            _onPickUpAction = entity.GetInteractAction();
            _triggerEventReceiver.OnEntered += OnTriggerEnter;
        }

        public void Dispose(in IEntity entity)
        {
            _triggerEventReceiver.OnEntered -= OnTriggerEnter;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(!collider.TryGetEntity(out IEntity character))return;
            if(!character.HasPlayerTag())return;                
            _onPickUpAction?.Invoke(character);
        }
    }
}