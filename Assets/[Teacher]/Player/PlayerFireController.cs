// using Leopotam.EcsLite;
// using Leopotam.EcsLite.Di;
//
// namespace SampleGame
// {
//     public sealed class PlayerFireController : IEcsRunSystem
//     {
//         private readonly EcsFilterInject<Inc<UnitFireRequired>> _units;
//         private readonly EcsSingletonInject<InputData> _inputData;
//         private readonly EcsUseCaseInject<TeamUseCase> _teamUseCase;
//
//         public void Run(IEcsSystems systems)
//         {
//             ref bool fire = ref _inputData.Value.isFire;
//             foreach (int entity in _units.Value)
//                 if (_teamUseCase.Value.IsTeam(entity, TeamType.BLUE))
//                     _units.Pools.Inc1.Get(entity).value = fire;
//         }
//     }
// }