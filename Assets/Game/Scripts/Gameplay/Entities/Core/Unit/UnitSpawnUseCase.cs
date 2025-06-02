namespace SampleGame
{
    public readonly struct UnitSpawnUseCase
    {
        public static void SpawnUnit(UnitSpawnType unitSpawnType, TeamType teamType)
        {
            var world = EcsAdmin.Systems.GetWorld();
            var filter = world.Filter<BaseTag>().End();
            var unitSpawnRequiredPool = world.GetPool<UnitSpawnRequired>();
            var team = world.GetPool<TeamType>();
            foreach (int entity in filter)
            {
                if(team.Get(entity) != teamType)
                    continue;
                ref UnitSpawnRequired required = ref unitSpawnRequiredPool.Get(entity);
                required.value = true;
                required.type = unitSpawnType;
            }
        }
    }
}