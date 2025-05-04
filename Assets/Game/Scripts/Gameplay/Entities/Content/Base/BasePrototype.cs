using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{

    [CreateAssetMenu(
        fileName = "Base",
        menuName = "SampleGame/Entities/New Base"
    )]
    public sealed class BasePrototype : EcsPrototype
    {
        [SerializeField]
        private int _health = 10;

        [SerializeField]
        private float3 _fireOffset = new(0, 0, 1);
        protected override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<BaseTag>().Add(entity);            
            world.GetPool<DeathableTag>().Add(entity);
            
            world.GetPool<UnitSpawnRequired>().Add(entity);
            
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