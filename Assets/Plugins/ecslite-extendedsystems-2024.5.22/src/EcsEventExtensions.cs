namespace Leopotam.EcsLite.ExtendedSystems
{
    public static class EcsEventExtensions
    {
        public static IEcsSystems ClearEvents<T>(this IEcsSystems systems, string worldName = null) where T : struct
        {
            return systems.GetWorld(worldName) == null
                ? throw new System.Exception($"Requested world \"{(string.IsNullOrEmpty(worldName) ? "[default]" : worldName)}\" not found.")
                : systems.Add(new ClearEventSystem<T>(systems.GetWorld(worldName)));
        }
    }
}