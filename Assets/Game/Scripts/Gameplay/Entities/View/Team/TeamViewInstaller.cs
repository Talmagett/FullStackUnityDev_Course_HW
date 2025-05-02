using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class TeamViewInstaller : EcsViewInstaller
    {
        [SerializeField]
        private Renderer[] _renderers;

        public override void Install(in EcsWorld world, in int entity)
        {
            world.GetPool<TeamView>().Add(entity).renderers = _renderers;
        }
    }
}