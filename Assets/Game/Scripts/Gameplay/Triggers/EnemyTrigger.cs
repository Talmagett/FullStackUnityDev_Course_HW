using System;
using Atomic.Entities;
using SampleGame;
using UnityEngine;

namespace Game.Gameplay
{
    [RequireComponent(typeof(Collider))]
    public sealed class EnemyTrigger : MonoBehaviour
    {
        [SerializeField]
        private SceneEntity[] _enemies;

        private void OnTriggerEnter(Collider other)
        {
            if(!other.TryGetEntity(out IEntity entity))return;
            if(!entity.HasPlayerTag())return;
            
            foreach (var enemy in _enemies)
            {
                enemy.GetTarget().Value=entity;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(!other.TryGetEntity(out IEntity entity))return;
            if(!entity.HasPlayerTag())return;
            
            foreach (var enemy in _enemies)
            {
                enemy.GetTarget().Value=null;
            }
        }
    }
}