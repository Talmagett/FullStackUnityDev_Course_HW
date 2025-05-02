// using Leopotam.EcsLite;
// using Unity.Mathematics;
// using UnityEngine;
//
// namespace SampleGame
// {
//     [CreateAssetMenu(
//         fileName = "Projectile",
//         menuName = "SampleGame/Entities/New Projectile"
//     )]
//     public sealed class ProjectilePrototype : EcsPrototype
//     {
//         [SerializeField]
//         private float _moveSpeed = 3;
//
//         [SerializeField]
//         private float _lifetime = 3;
//
//         [SerializeField]
//         private int _damage = 1;
//
//         protected override void Install(in EcsWorld world, in int entity)
//         {
//             world.GetPool<ProjectileTag>().Add(entity);
//             
//             //Move
//             world.GetPool<MoveableTag>().Add(entity);
//             world.GetPool<MoveSpeed>().Add(entity).value = _moveSpeed;
//             world.GetPool<MoveDirection>().Add(entity).value = new float3(0, 0, 1);
//
//             //Lifetime
//             world.GetPool<Lifetime>().Add(entity) = new Lifetime {value = _lifetime};
//             
//             //Damage
//             world.GetPool<Damage>().Add(entity).value = _damage;
//         }
//     }
// }