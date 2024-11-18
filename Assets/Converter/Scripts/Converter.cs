using System;
using System.ComponentModel;
using UnityEngine;

namespace Converter
{
    public class Converter
    {
        public bool IsWorking { get; private set; }
        public ConverterZone LoadZone { get; }
        public ConverterZone UnloadZone { get; }

        private int _convertingResourceCount;

        public bool OfResourceType(Resource checkingResource)
        {
            return _recipe.InputResource.GetType() == checkingResource.GetType();
        }

        private readonly Recipe _recipe;
        private readonly int _workingPackCount;
        private readonly float _convertingTime;

        private float _countdownTime;

        public Converter(Recipe recipe, int convertingTime, int inputLimit, int outputLimit, int workingPackCount)
        {
            if (convertingTime <= 0)
                throw new ArgumentOutOfRangeException(nameof(convertingTime), "must be greater than zero");

            if (inputLimit <= 0)
                throw new ArgumentOutOfRangeException(nameof(inputLimit), "must be greater than zero");

            if (outputLimit <= 0)
                throw new ArgumentOutOfRangeException(nameof(outputLimit), "must be greater than zero");

            if (workingPackCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(workingPackCount), "must be greater than zero");

            _recipe = recipe ?? throw new ArgumentNullException(nameof(recipe), "must be greater than zero");
            LoadZone = new ConverterZone(inputLimit);
            UnloadZone = new ConverterZone(outputLimit);
            _convertingTime = convertingTime;
            _workingPackCount = workingPackCount;
        }

        private void Convert()
        {
            var convertingCount = _convertingResourceCount / _recipe.InputCount * _recipe.OutputCount;
            Debug.Log($"{convertingCount} converting count");
            UnloadZone.PutResources(convertingCount);
            _convertingResourceCount = 0;
            IsWorking = false;
        }

        public void Update(float deltaTime)
        {
            if (TryWork()) ResetTimer();

            if (!IsWorking) return;

            _countdownTime -= deltaTime;
            if (_countdownTime <= 0)
                Convert();
        }

        public int LoadResources(Resource resource, int addingResourcesCount)
        {
            if (resource == null)
                throw new ArgumentNullException(nameof(resource), "Resource is null");
            if (!OfResourceType(resource))
                throw new InvalidEnumArgumentException("Resource type is not supported");

            var change = LoadZone.PutResources(addingResourcesCount);
            return change;
        }

        private bool TryWork()
        {
            if (IsWorking)
                return false;
            if (UnloadZone.IsFill())
                return false;
            if (LoadZone.IsEmpty())
                return false;
            if (LoadZone.GetResourcesCount() < _recipe.InputCount)
                return false;

            var convertingCount = _workingPackCount - (_workingPackCount % _recipe.InputCount);
            _convertingResourceCount = LoadZone.RemoveResources(convertingCount);

            IsWorking = true;
            return true;
        }

        public void TurnOff()
        {
            if (!IsWorking) return;

            IsWorking = false;
            if (_convertingResourceCount > 0) LoadZone.PutResources(_convertingResourceCount);
        }

        private void ResetTimer()
        {
            _countdownTime = _convertingTime;
        }
    }
}