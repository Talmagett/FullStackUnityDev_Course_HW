using System;

namespace Converter
{
    public class Recipe
    {
        public Resource InputResource { get; }
        public int InputCount { get; }
        public Resource OutputResource { get; }
        public int OutputCount { get; }

        public Recipe(Resource inputResource, int inputCount,Resource outputResource, int outputCount)
        {
            if(inputCount<=0)
                throw new ArgumentOutOfRangeException(nameof(inputCount), "must be greater than zero");
            if(outputCount<=0)
                throw new ArgumentOutOfRangeException(nameof(outputCount), "must be greater than zero");
            
            InputResource = inputResource ?? throw new ArgumentNullException(nameof(inputResource),"is null");
            InputCount = inputCount;
            OutputResource = outputResource ?? throw new ArgumentNullException(nameof(outputResource),"is null");
            OutputCount = outputCount;
        }
    }
}