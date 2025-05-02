using System;
using Unity.Mathematics;

namespace SampleGame
{
    [Serializable]
    public sealed class InputData
    {
        public float3 moveDirection;
        public bool isFire;
    }
}