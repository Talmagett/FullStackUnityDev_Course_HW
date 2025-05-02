using Leopotam.EcsLite;
using UnityEngine;

namespace SampleGame
{
    public sealed class MoveSpeedInstaller : EcsComponentInstaller<MoveSpeed>
    {
        [SerializeField]
        private float _moveSpeed;

        protected override MoveSpeed GetValue() => new() {value = _moveSpeed};
    }
}