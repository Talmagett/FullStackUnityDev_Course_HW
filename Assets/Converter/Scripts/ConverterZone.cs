using System;

namespace Converter
{
    public class ConverterZone
    {
        public int Limit { get;}
        public ResourceType ResourceType { get;}
        public int Count { get; private set; }

        public ConverterZone(int limit, ResourceType resource)
        {
            Limit = limit;
            ResourceType = resource;
        }

        public int AddResources(int addingResourcesCount)
        {
            if (addingResourcesCount <= 0)
                throw new ArgumentOutOfRangeException("AddingResourcesCount must be greater than zero");

            var newValue = Count + addingResourcesCount;
            if (newValue >= Limit)
            {
                Count = Limit;
                return newValue - Limit;
            }

            Count += addingResourcesCount;
            return 0;
        }
    }
}