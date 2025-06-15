using System;
using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    public sealed class GameSaveController : IInitializable, IDisposable, ITickable
    {
        private readonly ApplicationEvents _appEvents;
        private readonly GameSaveLoader _saveLoader;

        private readonly float _savePeriod;
        private float _remainingSeconds;

        public GameSaveController(ApplicationEvents appEvents, GameSaveLoader saveLoader, float savePeriod)
        {
            _appEvents = appEvents;
            _saveLoader = saveLoader;
            _savePeriod = savePeriod;
        }

        public void Initialize()
        {
            _remainingSeconds = _savePeriod;
            _appEvents.OnPaused += this.Save;
            _appEvents.OnQuit += this.Save;
        }

        public void Dispose()
        {
            _appEvents.OnPaused -= this.Save;
            _appEvents.OnQuit -= this.Save;
        }

        void ITickable.Tick()
        {
            _remainingSeconds -= Time.deltaTime;
            if (_remainingSeconds <= 0)
                this.Save();
        }

        private void Save()
        {
            _saveLoader.Save().Forget();
            _remainingSeconds = _savePeriod;
        }
    }
}