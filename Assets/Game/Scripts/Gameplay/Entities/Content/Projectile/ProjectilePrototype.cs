using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "Projectile",
        menuName = "SampleGame/Entities/New Projectile"
    )]
    public sealed class ProjectilePrototype : EcsPrototype
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private float _lifetime = 3;

        [SerializeField]
        private int _damage = 1;

        protected override void Install(in EcsWorld world, in int entity)
        {
            //Move
            world.GetPool<MoveableTag>().Add(entity);
            world.GetPool<MoveSpeed>().Add(entity).value = _moveSpeed;
            world.GetPool<MoveDirection>().Add(entity);
            
            //Lifetime
            world.GetPool<Lifetime>().Add(entity).value = _lifetime;
            
            //Damage
            world.GetPool<Damage>().Add(entity).value = _damage;
        }
    }
}