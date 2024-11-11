using System;
using System.ComponentModel;
using Atomic.Elements;
using UnityEngine;

namespace Converter
{
    public class Converter : IDisposable
    {
        public bool IsWorking { get; private set; }
        public ConverterZone LoadZone { get; }
        public ConverterZone UnloadZone { get; }
        
        private int _convertingResourceCount;
        
        private readonly Recipe _recipe;
        private readonly Countdown _countdown;
        
        public Converter(Recipe recipe, int convertingTime, ConverterZone loadZone, ConverterZone unloadZone)
        {
            _recipe = recipe;
            LoadZone = loadZone;
            UnloadZone = unloadZone;
            _countdown = new Countdown(convertingTime);
            _countdown.OnEnded += Convert;
        }

        private void Convert()
        {
            Debug.Log("Converting");
            UnloadZone.AddResources(_convertingResourceCount);
            _convertingResourceCount = 0;
            IsWorking = false;
        }

        public void Update(float deltaTime)
        {
            if (!IsWorking)
            {
                if (!TryWork())
                {
                    _countdown.Stop();
                }
            }
            _countdown.Tick(deltaTime);
        }

        public int LoadResources(Resource resource, int addingResourcesCount)
        {
            if(resource==null)
                throw new ArgumentNullException("Resource is null");
            if (!LoadZone.CanConvert(resource.ResourceType))
                throw new InvalidEnumArgumentException("Resource type is not supported");
            
            var change = LoadZone.AddResources(addingResourcesCount);
            return change;
        }

        private bool TryWork()
        {
            if (UnloadZone.IsFill())
                return false;
            if (LoadZone.IsEmpty())
                return false;

            IsWorking = true;
            _countdown.Start();
            _convertingResourceCount = LoadZone.RemoveResources();
            return true;
        }
        
        public void TurnOff()
        {
            if (!IsWorking) return;
            
            IsWorking = false;
            _countdown.Stop();
            _countdown.ResetTime();
            if (_convertingResourceCount > 0)
            {
                LoadZone.AddResources(_convertingResourceCount);
            }
        }

        public void Dispose()
        {
            _countdown.OnEnded -= Convert;
        }
    }
}