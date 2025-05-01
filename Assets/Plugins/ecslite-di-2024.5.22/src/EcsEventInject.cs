using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SampleGame
{
    public struct EcsEventInject<T> : IEcsDataInject where T : struct
    {
        public EcsEvent<T> Value;
        
        private string _worldName;

        public static implicit operator EcsEventInject<T>(string worldName) => 
            new() {_worldName = worldName};

        void IEcsDataInject.Fill(IEcsSystems systems) => 
            this.Value = systems.GetWorld(_worldName).GetEvent<T>();
    }
}