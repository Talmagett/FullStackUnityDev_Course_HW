using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "Swordman",
        menuName = "SampleGame/Entities/New Swordman"
    )]
    public sealed class SwordmanPrototype : EcsPrototype
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private float _rotationSpeed = 0.2f;

        [SerializeField] private float _attackDistance=3;
        [SerializeField] private float _attackCooldown = 1;
        [SerializeField] private int _damage=1;
        
        [SerializeField]
        private int _health = 5;
        
        protected override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<SwordmanTag>().Add(entity);
            world.GetPool<DeathableTag>().Add(entity);
            world.GetPool<UnitDirection>().Add(entity);
            world.GetPool<UnitAttackRequired>().Add(entity);
            
            //Move
            world.GetPool<MoveableTag>().Add(entity);
            world.GetPool<MoveSpeed>().Add(entity).value = _moveSpeed;
            world.GetPool<MoveDirection>().Add(entity);

            //Rotate
            world.GetPool<RotatableTag>().Add(entity);
            world.GetPool<RotateDirection>().Add(entity);
            world.GetPool<RotationSpeed>().Add(entity).value = _rotationSpeed;
            
            //Attack
            world.GetPool<AttackableTag>().Add(entity);
            world.GetPool<Damage>().Add(entity).value = _damage;
            world.GetPool<AttackDistance>().Add(entity).value = _attackDistance;
            world.GetPool<AttackCooldown>().Add(entity).max = _attackCooldown;

            //Health
            world.GetPool<Health>().Add(entity) = new Health
            {
                current = _health,
                max = _health
            };
            world.GetPool<Target>().Add(entity) = new Target
            {
                value = -1 // No target initially
            };
        }
    }
}