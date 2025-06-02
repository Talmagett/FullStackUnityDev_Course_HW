using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    [CreateAssetMenu(
        fileName = "Character",
        menuName = "SampleGame/Entities/New Character"
    )]
    public sealed class CharacterPrototype : EcsPrototype
    {
        [SerializeField]
        private float _moveSpeed = 3;

        [SerializeField]
        private float _rotationSpeed = 0.2f;

        [SerializeField]
        private float3 _fireOffset = new(0, 1, 1);

        [SerializeField]
        private int _health = 5;
        
        protected override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<CharacterTag>().Add(entity);
            world.GetPool<DeathableTag>().Add(entity);
            world.GetPool<UnitDirection>().Add(entity);
            world.GetPool<UnitFireRequired>().Add(entity);
            
            //Move
            world.GetPool<MoveableTag>().Add(entity);
            world.GetPool<MoveSpeed>().Add(entity).value = _moveSpeed;
            world.GetPool<MoveDirection>().Add(entity).value = new float3(0, 0, 1);

            //Rotate
            world.GetPool<RotatableTag>().Add(entity);
            world.GetPool<RotateDirection>().Add(entity).value = new float3(0, 0, -1);
            world.GetPool<RotationSpeed>().Add(entity).value = _rotationSpeed;
            
            //Fire
            world.GetPool<FireOffset>().Add(entity).value = _fireOffset;
            
            //Health
            world.GetPool<Health>().Add(entity) = new Health
            {
                current = _health,
                max = _health
            };
        }
    }
}