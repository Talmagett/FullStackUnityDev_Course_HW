using System;

namespace Converter
{
    public class ConverterZone
    {
        private readonly ResourceType _resourceType;
        private readonly int _limit;
        private readonly int _convertingCount;
        private int _count;

        public ConverterZone(ResourceType recipeInputResourceType, int limit, int convertingCount)
        {
            _limit = limit;
            _resourceType = recipeInputResourceType;
            _convertingCount = convertingCount;
        }

        public bool CanConvert(ResourceType inputResourceType) => _resourceType == inputResourceType;
        public int GetResourcesCount() => _count;
        public bool IsFill() => _count == _limit;
        public bool IsEmpty() => _count ==0;

        public int PutResources(int addingResourcesCount)
        {
            if (addingResourcesCount <= 0)
                throw new ArgumentOutOfRangeException("AddingResourcesCount must be greater than zero");
            
            var newValue = _count + addingResourcesCount;
            if (newValue >= _limit)
            {
                _count = _limit;
                return newValue - _limit;
            }

            _count += addingResourcesCount;
            return 0;
        }

        public int RemoveResources()
        {
            var removingCount =  _count > _convertingCount ? _convertingCount : _count;
            _count -= removingCount;
            return removingCount;
        }
    }
}