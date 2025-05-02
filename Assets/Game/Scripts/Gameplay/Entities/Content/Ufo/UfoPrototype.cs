using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{

    [CreateAssetMenu(
        fileName = "Ufo",
        menuName = "SampleGame/Entities/New Ufo"
    )]
    public sealed class UfoPrototype : EcsPrototype
    {
        [SerializeField]
        private float _moveSpeed = 3;

        protected override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<UfoTag>().Add(entity);
            world.GetPool<UnitDirection>().Add(entity);
            
            world.GetPool<MoveableTag>().Add(entity);
            world.GetPool<MoveSpeed>().Add(entity).value = _moveSpeed;
            world.GetPool<MoveDirection>().Add(entity).value = new float3(0, 0, 1);
        }
    }
}