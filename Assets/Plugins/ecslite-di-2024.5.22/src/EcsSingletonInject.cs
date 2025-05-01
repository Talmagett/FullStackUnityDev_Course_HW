namespace Leopotam.EcsLite.Di
{
    public struct EcsSingletonInject<T> : IEcsDataInject where T : class
    {
        public T Value;

        private string _worldName;

        public static implicit operator EcsSingletonInject<T>(string worldName) =>
            new() {_worldName = worldName};

        void IEcsDataInject.Fill(IEcsSystems systems) =>
            this.Value = systems.GetWorld(_worldName).GetSingleton<T>();
    }
}