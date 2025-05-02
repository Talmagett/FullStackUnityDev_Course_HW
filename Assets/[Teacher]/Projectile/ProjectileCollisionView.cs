// using Leopotam.EcsLite;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class ProjectileCollisionView : MonoBehaviour
//     {
//         [SerializeField]
//         private EcsView _view;
//
//         private void OnTriggerEnter(Collider other)
//         {
//             if (!other.TryGetComponent(out EcsView target))
//                 return;
//
//             var collisionRequests = EcsAdmin.Systems.GetWorld().GetEvent<ProjectileCollisionRequest>();
//             collisionRequests.Fire(new ProjectileCollisionRequest
//             {
//                 projectile = _view.GetPackedEntity(),
//                 target = target.GetPackedEntity()
//             });
//         }
//     }
// }