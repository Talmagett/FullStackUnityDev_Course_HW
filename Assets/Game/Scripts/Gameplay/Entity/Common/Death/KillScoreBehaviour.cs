using System;
using Atomic.Elements;
using Atomic.Entities;
using Game.Scripts.Gameplay.Context;
using Modules.Gameplay;
using UnityEngine;

namespace SampleGame
{
    public sealed class KillScoreBehaviour : IEntityInit, IEntityDispose
    {
        private Health _health;
        private GameObject _gameObject;
        private IReactive _deathEvent;
        private ReactiveInt _killScore;
        private IGameContext _gameContext;
        public KillScoreBehaviour(GameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public void Init(in IEntity entity)
        {
            _gameObject = entity.GetGameObject();
            _deathEvent = entity.GetDeathEvent();
            _killScore = _gameContext.GetKillScore();
            
            _deathEvent.Subscribe(this.OnDeath);
        }

        public void Dispose(in IEntity entity)
        {
            _deathEvent.Unsubscribe(this.OnDeath);
        }

        private void OnDeath()
        {
            Debug.Log($"KillScoreBehaviour.OnDeath: {_gameObject.name} {_killScore!=null}");
            _killScore.Value++;
        }
    }
}