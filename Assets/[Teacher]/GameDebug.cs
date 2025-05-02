// using Leopotam.EcsLite;
// using Sirenix.OdinInspector;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class GameDebug : MonoBehaviour
//     {
//
//         private IEcsSystems _ecsSystems;
//
//         private void Start()
//         {
//             _ecsSystems = EcsAdmin.Systems;
//         }
//
//         [Button]
//         public void SpawnEntity(SpawnRequest request)
//         {
//             _ecsSystems.GetWorld().GetEvent<SpawnRequest>().Fire(request);
//         }
//
//         [Button]
//         public void DespawnEntity(DespawnRequest request)
//         {
//             EcsWorld eventWorld = _ecsSystems.GetWorld(EcsConsts.EventWorld);
//             int evt = eventWorld.NewEntity();
//             eventWorld.GetPool<DespawnRequest>().Add(evt) = request;
//         }
//     }
// }