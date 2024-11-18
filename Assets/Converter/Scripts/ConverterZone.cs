using System;
using UnityEngine;

namespace Converter
{
    public class ConverterZone
    {
        private readonly int _limit;
        private int _count;

        public ConverterZone(int limit)
        {
            if (limit < 1)
                throw new ArgumentOutOfRangeException($"limit must be greater than zero");
            _limit = limit;
        }

        public int GetResourcesCount() => _count;
        public bool IsFill() => _count == _limit;
        public bool IsEmpty() => _count ==0;

        public int PutResources(int addingResourcesCount)
        {
            if (addingResourcesCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(addingResourcesCount),"AddingResourcesCount must be greater than zero");
            
            var newValue = _count + addingResourcesCount;
            if (newValue > _limit)
            {
                _count = _limit;
                return newValue - _limit;
            }

            _count += addingResourcesCount;
            return 0;
        }

        public int RemoveResources(int amount)
        {
            if (amount<=0)
                throw new ArgumentOutOfRangeException(nameof(amount),"Amount must be greater than zero");

            var removingValue = amount > _count?_count:amount;
            _count -= removingValue;
            return removingValue;
        }
    }
}