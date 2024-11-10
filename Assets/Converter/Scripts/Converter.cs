namespace Converter
{
    public class Converter
    {
        public bool IsWorking { get; private set; }

        private readonly int _convertingTime;
        private ConverterZone _loadZone;
        private ConverterZone _unloadZone;
        private readonly Recipe _recipe;

        public Converter(Recipe recipe, int convertingTime, int loadLimit, int unloadLimit)
        {
            _convertingTime = convertingTime;
            _recipe = recipe;
            _loadZone = new ConverterZone(loadLimit,recipe.InputResourceType);
            _unloadZone = new ConverterZone(unloadLimit, recipe.OutputResourceType);
        }


        public bool LoadResources(Resource resource, int addingResourcesCount)
        {
            if (_loadZone.ResourceType != resource.ResourceType)
                return false;
            
            if (addingResourcesCount > _loadZone.Limit)
                return false;

            _loadZone.AddResources(addingResourcesCount);
            return true;
        }
    }
}