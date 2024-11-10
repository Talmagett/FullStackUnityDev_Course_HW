namespace Converter
{
    public class Recipe
    {
        public readonly ResourceType InputResourceType;
        public readonly int InputCount;
        public readonly ResourceType OutputResourceType;
        public readonly int OutputCount;

        public Recipe(ResourceType inputResourceType, int inputCount, ResourceType outputResourceType, int outputCount)
        {
            InputResourceType = inputResourceType;
            InputCount = inputCount;
            OutputResourceType = outputResourceType;
            OutputCount = outputCount;
        }
    }
}