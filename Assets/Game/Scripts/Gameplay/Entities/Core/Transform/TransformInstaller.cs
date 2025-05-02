using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class TransformInstaller : MonoBehaviour, IEcsEntityInstaller
    {
        public void Install(EcsWorld world, int entity)
        {
            world.GetPool<Position>().Add(entity).value = this.transform.position;
            world.GetPool<Rotation>().Add(entity).value = this.transform.rotation;
        }
    }
}